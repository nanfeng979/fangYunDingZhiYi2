using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Y9g;

public class TiesManager : Singleton<TiesManager>
{
    [SerializeField]
    private GameObject tiesListGameObject;

    public GameObject GetTiesListGameObject()
    {
        return tiesListGameObject;
    }

    private GameObject[] tieObjectList = new GameObject[2]; // 羁绊的显示对象
    private string[] tiesNameArray = new string[2]; // 按顺序登记羁绊名字
    private Dictionary<string, int> tiesStatusDic = new Dictionary<string, int>(); // 记录羁绊是否激活


    

    private void Start() {
        InitTiesStatusDic(); // 初始化羁绊状态字典
        LoadTiesList();

        ActiveTie(new List<string> { "Jushen" });
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            ActiveTie(new List<string> { "Huwei" });
        }
    }

    /// <summary>
    /// 初始化羁绊状态字典
    /// </summary>
    private void InitTiesStatusDic()
    {
        tiesStatusDic.Add("Huwei", 0);
        tiesStatusDic.Add("Jushen", 0);
        tiesStatusDic.Add("Mozhiying", 0);
        tiesStatusDic.Add("Qingtianwei", 0);
        tiesStatusDic.Add("Youhun", 0);
    }

    /// <summary>
    /// 加载羁绊列表
    /// </summary>
    private void LoadTiesList() {
        for (int i = 0; i < tiesListGameObject.transform.childCount; i++) {
            tieObjectList[i] = tiesListGameObject.transform.GetChild(i).gameObject;
            tiesNameArray[i] = "";
        }
    }

    public void ActiveTie(List<string> tiesList) {
        for (int i = 0; i < tiesList.Count; i++) {
            // 如果羁绊没有被激活
            if (tiesStatusDic[tiesList[i]] == 0)
            {
                // 羁绊激活的位置
                // int indexOfTiesList = 0;
                // 激活羁绊
                // ActiveOneTieUI(tiesList[i], i);
                ActiveTieOneLogic(tiesList[i]);
            }
            // 如果羁绊已经被激活
            else
            {
                // 更新羁绊UI
            }

            // 激活羁绊，逻辑层
            // ActiveTieLogic(tiesList[i]);
        }
    }


    /// <summary>
    /// 激活羁绊，逻辑层
    /// </summary>
    /// <param name="tieName"></param>
    private void ActiveTieLogic(string tieName)
    {
        tiesStatusDic[tieName] += 1; // 激活羁绊
    }

    private void ActiveTieOneLogic(string tieName)
    {
        tiesStatusDic[tieName] = 1; // 激活羁绊

        tiesUIList.Add(TiesUISerializableFactory.CreateTiesUI(tieName));
        SortTiesUIList();
        ShowTiesUI();
    }

    private void ShowTiesUI()
    {
        for (int i = 0; i < tiesUIList.Count; i++)
        {
            GameObject tieObject = tieObjectList[i];
            tieObject.GetComponent<Image>().sprite = tiesUIList[i].TieSprite;
            tieObject.GetComponent<Image>().enabled = true; // 激活羁绊UI的Image组件
        }
    }

    /// <summary>
    /// 激活羁绊，UI层
    /// </summary>
    /// <param name="tieName"></param>
    /// <param name="indexOfTiesList"></param>
    private void ActiveOneTieUI(string tieName, int indexOfTiesList)
    {
        GameObject tieObject = tieObjectList[indexOfTiesList];
        Sprite tieSprite = TieSpriteMap.Instance.GetSprite(tieName);
        tieObject.GetComponent<Image>().sprite = tieSprite;
        tieObject.GetComponent<Image>().enabled = true; // 激活羁绊UI的Image组件
    }


    //////////////////////////
    private List<TiesUI> tiesUIList = new List<TiesUI>();

    private void SortTiesUIList()
    {
        tiesUIList.Sort((a, b) => {
            if (a.tiesUIPriority < b.tiesUIPriority)
            {
                return 1;
            }
            else if (a.tiesUIPriority > b.tiesUIPriority)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        });
    }


}

public class TiesUI
{
    public TiesUIPriority tiesUIPriority;
    public string tieName;
    public int tieCount;
    private Sprite tieSprite;
    public Sprite TieSprite
    {
        get
        {
            if (tieSprite == null)
            {
                tieSprite = TieSpriteMap.Instance.GetSprite(tieName);
            }
            return tieSprite;
        }
    }
}

public enum TiesUIPriority
{
    gray,
    copper,
    silvery,
    red,
    glod,
    diamond,
}

public class TiesUISerializableFactory
{
    public static TiesUI CreateTiesUI(string tieName)
    {
        switch (tieName)
        {
            case "Huwei":
                return new TiesUI()
                {
                    tiesUIPriority = TiesUIPriority.glod,
                    tieName = "Huwei",
                    tieCount = 1
                };
            case "Jushen":
                return new TiesUI()
                {
                    tiesUIPriority = TiesUIPriority.gray,
                    tieName = "Jushen",
                    tieCount = 1
                };
            case "Mozhiying":
                return new TiesUI()
                {
                    tiesUIPriority = TiesUIPriority.gray,
                    tieName = "Mozhiying",
                    tieCount = 1
                };
            case "Qingtianwei":
                return new TiesUI()
                {
                    tiesUIPriority = TiesUIPriority.gray,
                    tieName = "Qingtianwei",
                    tieCount = 1
                };
            case "Youhun":
                return new TiesUI()
                {
                    tiesUIPriority = TiesUIPriority.gray,
                    tieName = "Youhun",
                    tieCount = 1
                };
            default:
                return null;
        }
    }
}