using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using NativeWebSocket;
using Y9g;

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
            // Reading a plain text message
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            // 将message按冒号分开
            string[] messageArray = message.Split(':');

            // if (messageArray[0] != GameManager.Instance.GetPlayerName())
            // {
            //     return;
            // } 
            ChessObject chessObject = GameObject.Find(messageArray[1] + "(Clone)")?.GetComponent<ChessObject>();
            if (chessObject == null)
            {
                Debug.LogError("chessObject is null");
                return;
            }

            if (messageArray[2] == "AddEquipmentColumn")
            {
                chessObject.AddEquipmentColumnOnlyShow(int.Parse(messageArray[3]), messageArray[4]);
            }
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
}
