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

    private GameObject[] tieObjectList = new GameObject[8]; // 羁绊的显示对象
    private string[] tiesNameArray = new string[8]; // 按顺序登记羁绊名字
    private Dictionary<string, int> tiesStatusDic = new Dictionary<string, int>(); // 记录羁绊是否激活
    

    private void Start() {
        InitTiesStatusDic(); // 初始化羁绊状态字典
        LoadTiesList();

        ActiveTie(new List<string> { "Jushen" });
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            ActiveTie(new List<string> { "Jushen" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            ActiveTie(new List<string> { "Huwei" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) {
            ActiveTie(new List<string> { "Mozhiying" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            ActiveTie(new List<string> { "Qingtianwei" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad5)) {
            ActiveTie(new List<string> { "Youhun" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad6)) {
            ActiveTie(new List<string> { "Juedoudashi" });
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
        tiesStatusDic.Add("Juedoudashi", 0);
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
                ActiveTieFirstLogic(tiesList[i]);
            }
            // 如果羁绊已经被激活
            else
            {
                // 找到羁绊的位置，增加羁绊数量
                tiesUIList.Find((tiesUI) => {
                    if (tiesUI.tieName == tiesList[i])
                    {
                        tiesUI.TieCount += 1;
                        Debug.Log(tiesUI.TieCount);
                        return true;
                    }
                    return false;
                });
            }

            // 激活羁绊，逻辑层
            // ActiveTieLogic(tiesList[i]);
        }

        SortTiesUIList(); // 排序羁绊UI列表 // TODO: 后面要减少排序次数
        UpdateTiesUI(); // 更新羁绊UI // TODO: 后面要减少刷新次数
    }


    /// <summary>
    /// 激活羁绊，逻辑层
    /// </summary>
    /// <param name="tieName"></param>
    private void ActiveTieLogic(string tieName)
    {
        tiesStatusDic[tieName] += 1; // 激活羁绊
    }

    /// <summary>
    /// 第一次激活羁绊，逻辑层
    /// </summary>
    /// <param name="tieName"></param>
    private void ActiveTieFirstLogic(string tieName)
    {
        tiesStatusDic[tieName] = 1; // 激活羁绊

        tiesUIList.Add(TiesUISerializableFactory.CreateTiesUI(tieName));
    }

    /// <summary>
    /// 更新羁绊UI
    /// </summary>
    /// TODO: 目前是整体刷新，后续可能会有其他刷新方式
    private void UpdateTiesUI()
    {
        for (int i = 0; i < tiesUIList.Count; i++)
        {
            GameObject tieObject = tieObjectList[i];
            if (tiesUIList[i].TieSprite != null)
            {
                tieObject.GetComponent<Image>().sprite = tiesUIList[i].TieSprite;
            }
            tieObject.GetComponent<Image>().enabled = true; // 激活羁绊UI的Image组件

            Text text = tieObject.transform.GetChild(0).GetComponent<Text>();
            text.enabled = true;
            text.text = tiesUIList[i].TieCount.ToString();
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
        Sprite tieSprite = TieSpriteMap.Instance.GetSprite(tieName, 0);
        tieObject.GetComponent<Image>().sprite = tieSprite;
        tieObject.GetComponent<Image>().enabled = true; // 激活羁绊UI的Image组件
    }


    //////////////////////////
    private List<TiesUI> tiesUIList = new List<TiesUI>();

    /// <summary>
    /// 排序羁绊UI列表
    /// </summary>
    /// TODO: 目前是按照优先级排序，后续可能会有其他排序方式
    private void SortTiesUIList()
    {
        tiesUIList.Sort((a, b) => {
            if (a.TiesUIPriority < b.TiesUIPriority)
            {
                // if (a.tieCount > b.tieCount)
                // {
                //     return -1;
                // }
                // else if (a.tieCount < b.tieCount)
                // {
                //     return 1;
                // }
                // else
                // {
                //     return 0;
                // }
                return 1;
            }
            else if (a.TiesUIPriority > b.TiesUIPriority)
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
    private TiesUIPriority tiesUIPriority;
    public TiesUIPriority TiesUIPriority
    {
        get
        {
            return tiesUIPriority;
        }
        set
        {
            tiesUIPriority = value;
        }
    }
    public string tieName;
    private int tieCount;
    public int TieCount
    {
        get
        {
            return tieCount;
        }
        set
        {
            if (value < 0)
            {
                tieCount = 0;
            }
            else
            {
                tieCount = value;
                for (int i = 0; i < LevelStep.Count; i++)
                {
                    if (value >= LevelStep[i])
                    {
                        tiesUIPriority = (TiesUIPriority)i;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
    private Sprite tieSprite;
    public Sprite TieSprite
    {
        get
        {
            Sprite tieSpriteTemp = TieSpriteMap.Instance.GetSprite(tieName, (int)tiesUIPriority);
            if (tieSpriteTemp != null)
            {
                return tieSpriteTemp;
            }

            return tieSprite;
        }
    }

    private List<int> levelStep = new List<int>() { 0, 2, 4, 6, 8 };
    public List<int> LevelStep
    {
        get
        {
            return levelStep;
        }
    }

    private TiesUI() { }

    public TiesUI(string tieName, List<int> levelStep = null)
    {
        tiesUIPriority = TiesUIPriority.gray;
        this.tieName = tieName;
        tieCount = 1;
        if (levelStep != null)
        {
            this.levelStep = levelStep;
        }
    }
}

public enum TiesUIPriority
{
    gray,
    copper,
    silvery,
    // red,
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
                return new TiesUI("Huwei")
                {
                };
            case "Jushen":
                return new TiesUI("Jushen")
                {
                };
            case "Mozhiying":
                return new TiesUI("Mozhiying")
                {
                };
            case "Qingtianwei":
                return new TiesUI("Qingtianwei")
                {
                };
            case "Youhun":
                return new TiesUI("Youhun")
                {
                };
            case "Juedoudashi":
                return new TiesUI("Juedoudashi")
                {
                };
            default:
                return null;
        }
    }
}