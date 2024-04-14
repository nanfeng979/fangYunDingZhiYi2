using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Y9g;

public class TiesManager : Singleton<TiesManager>
{
    [SerializeField]
    private GameObject tiesListGameObject;

    private GameObject[] tieObjectList = new GameObject[8]; // 羁绊的显示对象
    private Dictionary<string, bool> tiesStatusDic = new Dictionary<string, bool>(); // 记录羁绊是否激活
    
    private List<TiesUI> tiesUIList = new List<TiesUI>(); // 羁绊UI列表

    private void Start() {
        InitTiesStatusDic(); // 初始化羁绊状态字典
        LoadTiesList();

        // ActiveTie(new List<string> { "Jushen" });
    }

    private void Update() {
        ListenerTieUIHeight(); // 监听羁绊UI的高度

        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            ActiveTie(new List<string> { "Shanhaihuijuan" });
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
        tiesStatusDic.Add("Shanhaihuijuan", false);
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
            Image tieObjectIconImage = tieObject.transform.Find("TieIcon").GetComponent<Image>(); // 获取羁绊UI的IconImage组件
            Text tieObjectText = tieObject.transform.GetChild(0).GetComponent<Text>(); // 获取羁绊UI的Text组件

            // 如果羁绊UI列表中有羁绊
            if (i < tiesUIList.Count)
            {
                tieObjectImage.enabled = true; // 激活羁绊UI的Image组件
                tieObjectIconImage.enabled = true; // 激活羁绊UI的IconImage组件
                tieObjectText.enabled = true; // 激活羁绊UI的Text组件

                tieObjectImage.color = tiesUIList[i].TieColor; // 更新羁绊颜色
                tieObjectIconImage.sprite = tiesUIList[i].TieSprite; // 更新羁绊Icon图片
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

    /// <summary>
    /// 监听羁绊UI的高度
    /// </summary>
    private void ListenerTieUIHeight()
    {
        // 已经激活的羁绊个数
        int activeTieCount = tiesUIList.Count;
        Debug.Log("activeTieCount: " + activeTieCount);

        if (activeTieCount <= 2)
        {
            ChangeTieUILength(100);
            ChangeTiesListHeight(-300);
        }
        else if (activeTieCount <= 4)
        {
            ChangeTieUILength(200);
            ChangeTiesListHeight(-200);
        }
        else if (activeTieCount <= 6)
        {
            ChangeTieUILength(300);
            ChangeTiesListHeight(-100);
        }
        else if (activeTieCount <= 8)
        {
            ChangeTieUILength(400);
            ChangeTiesListHeight(0);
        }
    }

    /// <summary>
    /// 改变羁绊UI的高度
    /// </summary>
    /// <param name="height"></param>
    private void ChangeTieUILength(float height)
    {
        GameObject TieCenter = transform.Find("TieCenter").gameObject;
        Vector2 TieCenterSize = TieCenter.GetComponent<RectTransform>().sizeDelta;
        TieCenterSize.y = height;
        TieCenter.GetComponent<RectTransform>().sizeDelta = TieCenterSize;
        float TieCenterHeight = TieCenter.GetComponent<RectTransform>().rect.height;

        // 调整
        GameObject TieTop = transform.Find("TieTop").gameObject;
        Vector3 TieTopPosition = TieTop.GetComponent<RectTransform>().anchoredPosition;
        float TieTopHeight = TieTop.GetComponent<RectTransform>().rect.height;
        TieTopPosition.y = TieCenterHeight + TieTopHeight;
        TieTop.GetComponent<RectTransform>().anchoredPosition = TieTopPosition;

        GameObject TieBottom = transform.Find("TieBottom").gameObject;
        Vector3 TieBottomPosition = TieBottom.GetComponent<RectTransform>().anchoredPosition;
        float TieBottomHeight = TieBottom.GetComponent<RectTransform>().rect.height;
        TieBottomPosition.y = -TieCenterHeight - TieBottomHeight;
        TieBottom.GetComponent<RectTransform>().anchoredPosition = TieBottomPosition;
    }

    /// <summary>
    /// 改变羁绊列表的高度
    /// </summary>
    /// <param name="height"></param>
    private void ChangeTiesListHeight(float height)
    {
        tiesListGameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, height, 0);
    }

    /// <summary>
    /// 获取羁绊列表对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetTiesListGameObject()
    {
        return tiesListGameObject;
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
            if (tieSprite != null)
            {
                return tieSprite;
            }
            tieSprite = TieSpriteMap.Instance.GetSprite(tieName);
            return tieSprite;
        }
    }
    private Color tieColor;
    public Color TieColor
    {
        get
        {
            switch (tiesUIPriority)
            {
                case TiesUIPriority.gray:
                    tieColor = new Color(0.5f, 0.5f, 0.5f, 1);
                    break;
                case TiesUIPriority.copper:
                    tieColor = Utils.HexToColor("#d38f6f");
                    break;
                case TiesUIPriority.silvery:
                    tieColor = Utils.HexToColor("#a2b2b2");
                    break;
                case TiesUIPriority.glod:
                    tieColor = Utils.HexToColor("#e7c976");
                    break;
                case TiesUIPriority.diamond:
                    tieColor = Utils.HexToColor("#e3a0eb");
                    break;
                default:
                    tieColor = new Color(0.5f, 0.5f, 0.5f, 1);
                    break;
            }
            return tieColor;
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
            case "Shanhaihuijuan":
                return new TiesUI("Shanhaihuijuan", new List<int> { 0, 3, 5, 7, 10 })
                {
                };
            default:
                return null;
        }
    }
}