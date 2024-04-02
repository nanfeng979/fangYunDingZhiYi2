using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;
using Y9g;
using System;

public class Guangbo : Singleton<Guangbo>
{
    WebSocket websocket;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://localhost:8080");

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            // 将message按冒号分开
            string[] messageArray = message.Split(':');

            // [0]: 玩家名称
            // [1]: 云顶之弈对象ID
            // [2]: 云顶之弈对象类型
            // [3]: 方法名称
            // [4~N]: 参数

            // 获取对象
            GameObject gameObject = IDToGameObjectMap.Instance.GetObject(int.Parse(messageArray[1]));
            if (gameObject == null)
            {
                Debug.LogError("gameObject is null");
                return;
            }

            // 获取对象的网络组件
            object networkObject = null;
            if (messageArray[2] == YunDingZhiYiBaseObjectType.Chess.ToString())
            {
                networkObject = gameObject.GetComponent<ChessObject_NetWork_Interface>();
            }
            else if (messageArray[2] == YunDingZhiYiBaseObjectType.Prop.ToString())
            {
                networkObject = gameObject.GetComponent<Prop_NetWork_Interface>();
            }
            if (networkObject == null)
            {
                Debug.LogError("networkObject is null");
                return;
            }
            
            // 获取方法
            Type type = networkObject.GetType();
            System.Reflection.MethodInfo method = type.GetMethod(messageArray[3] + "_Network");
            if (method == null)
            {
                Debug.LogError("method is null");
                return;
            }

            // 获取参数
            string[] _params = new string[messageArray.Length - 4];
            for (int i = 4; i < messageArray.Length; i++)
            {
                _params[i - 4] = messageArray[i];
            }

            // 调用方法
            method.Invoke(networkObject, _params);
        };

        // Waiting for connections to complete
        await websocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
        #endif

        if (Input.GetKeyDown(KeyCode.S))
        {
            _SendMessage(GameManager.Instance.GetPlayerName() + " 发送消息");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            IDToGameObjectMap.Instance.PrintAll();
        }
    }

    public async void _SendMessage(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            await websocket.SendText(message);
        }
        else
        {
            Debug.Log("WebSocket is not open!");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    // 构造广播内容
    public string ConstructGuangboContent(string playerName, int objectID, YunDingZhiYiBaseObjectType objectType, string methodName, params string[] _params)
    {
        string guangboContent = playerName + ":"; // 玩家所属
        guangboContent += objectID.ToString() + ":"; // 云顶之弈对象ID
        guangboContent += objectType.ToString() + ":"; // 云顶之弈对象类型
        guangboContent += methodName + ":"; // 方法名称
        for (int i = 0; i < _params.Length; i++)
        {
            guangboContent += _params[i] + ":";
        }
        guangboContent = guangboContent.Substring(0, guangboContent.Length - 1); // 去掉最后一个冒号

        return guangboContent;
    }
}
