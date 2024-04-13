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
    private Dictionary<string, bool> tiesStatusDic = new Dictionary<string, bool>(); // 记录羁绊是否激活
    
    private List<TiesUI> tiesUIList = new List<TiesUI>(); // 羁绊UI列表

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

        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            ActiveTie(new List<string> { "Jianzhi" });
        }

        if (Input.GetKeyDown(KeyCode.Keypad8)) {
            UnActiveTie("Jianzhi");
        }
    }

    /// <summary>
    /// 初始化羁绊状态字典
    /// </summary>
    private void InitTiesStatusDic()
    {
        tiesStatusDic.Add("Huwei", false);
        tiesStatusDic.Add("Jushen", false);
        tiesStatusDic.Add("Mozhiying", false);
        tiesStatusDic.Add("Qingtianwei", false);
        tiesStatusDic.Add("Youhun", false);
        tiesStatusDic.Add("Juedoudashi", false);
        tiesStatusDic.Add("Jianzhi", false);
    }

    /// <summary>
    /// 加载羁绊列表
    /// </summary>
    private void LoadTiesList() {
        for (int i = 0; i < tiesListGameObject.transform.childCount; i++) {
            tieObjectList[i] = tiesListGameObject.transform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// 激活羁绊
    /// </summary>
    /// <param name="tiesList"></param>
    public void ActiveTie(List<string> tiesList) {
        for (int i = 0; i < tiesList.Count; i++) {
            // 如果羁绊没有被激活
            if (tiesStatusDic[tiesList[i]] == false)
            {
                ActiveTieFirstLogic(tiesList[i]);
            }
            // 如果羁绊已经被激活
            else
            {
                // 找到羁绊的排序位置，增加羁绊数量
                tiesUIList.Find((tiesUI) => {
                    if (tiesUI.tieName == tiesList[i])
                    {
                        tiesUI.TieCount += 1;
                        return true;
                    }
                    return false;
                });
            }
        }

        SortTiesUIList(); // 排序羁绊UI列表 // TODO: 后面要减少排序次数
        UpdateTiesUI(); // 更新羁绊UI // TODO: 后面要减少刷新次数
    }

    /// <summary>
    /// 取消激活羁绊
    /// </summary>
    /// <param name="tieName"></param>
    public void UnActiveTie(string tieName) {
        tiesUIList.Find((tiesUI) => {
            if (tiesUI.tieName == tieName)
            {
                tiesUI.TieCount -= 1;
                if (tiesUI.TieCount == 0)
                {
                    tiesStatusDic[tiesUI.tieName] = false;
                    tiesUIList.Remove(tiesUI);
                }
                return true;
            }
            return false;
        });

        SortTiesUIList(); // 排序羁绊UI列表
        UpdateTiesUI(); // 更新羁绊UI
    }

    /// <summary>
    /// 第一次激活羁绊，逻辑层
    /// </summary>
    /// <param name="tieName"></param>
    private void ActiveTieFirstLogic(string tieName)
    {
        tiesStatusDic[tieName] = true; // 激活羁绊

        tiesUIList.Add(TiesUISerializableFactory.CreateTiesUI(tieName)); // 在羁绊UI列表中添加羁绊
    }

    /// <summary>
    /// 更新羁绊UI
    /// </summary>
    private void UpdateTiesUI()
    {
        for (int i = 0; i < tieObjectList.Length; i++)
        {
            GameObject tieObject = tieObjectList[i]; // 获取羁绊UI对象
            Image tieObjectImage = tieObject.GetComponent<Image>(); // 获取羁绊UI的Image组件
            Text tieObjectText = tieObject.transform.GetChild(0).GetComponent<Text>(); // 获取羁绊UI的Text组件

            // 如果羁绊UI列表中有羁绊
            if (i < tiesUIList.Count)
            {
                tieObjectImage.enabled = true; // 激活羁绊UI的Image组件
                tieObjectText.enabled = true; // 激活羁绊UI的Text组件

                tieObjectImage.sprite = tiesUIList[i].TieSprite; // 更新羁绊图片
                tieObjectText.text = tiesUIList[i].TieCount.ToString(); // 更新羁绊数量
            }
            // 如果羁绊UI列表中没有羁绊
            else
            {
                tieObjectImage.enabled = false; // 关闭羁绊UI的Image组件
                tieObjectText.enabled = false; // 关闭羁绊UI的Text组件
            }
        }
    }


    /// <summary>
    /// 排序羁绊UI列表
    /// </summary>
    private void SortTiesUIList()
    {
        tiesUIList.Sort((a, b) => {
            if (a.TiesUIPriority < b.TiesUIPriority)
            {
                return 1;
            }
            else if (a.TiesUIPriority >= b.TiesUIPriority)
            {
                if (a.TieCount >= b.TieCount)
                {
                    return -1;
                }
                else if (a.TieCount < b.TieCount)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
                // return -1;
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
                tieSprite = tieSpriteTemp;
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
    gray = 0,
    copper = 1,
    silvery = 2,
    // red,
    glod = 3,
    diamond = 4,
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
            case "Jianzhi":
                return new TiesUI("Jianzhi", new List<int> { 0, 3, 5, 7, 10 })
                {
                };
            default:
                return null;
        }
    }
}