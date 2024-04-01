using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Y9g;

public class HPObjectPool : Singleton<HPObjectPool>
{
    private Queue<GameObject> loseHPPool = new Queue<GameObject>(); //使用队列，先进先出，避免连续生成同一个对象
    public Queue<GameObject> LoseHPPool { get { return loseHPPool; } }
    [SerializeField]
    private GameObject loseHPPrefab; // 失去血量的预制体

    void Start()
    {
        ExpandLoseHP(loseHPPrefab, 20);
    }

    /// <summary>
    /// 扩展对象池
    /// </summary>
    /// <param name="count"></param>
    /// <param name="prefab"></param>
    public void ExpandLoseHP(GameObject prefab, int count)
    {
        GameObject obj = null;
        for (int index = 0; index < count; index++)
        {
            obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            loseHPPool.Enqueue(obj);
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="parentTransform"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public GameObject GetObject(Transform parentTransform, float damage)
    {
        if (loseHPPool.Count <= 0)
            ExpandLoseHP(loseHPPrefab, 20);
        GameObject obj = loseHPPool.Dequeue();
        obj.transform.SetParent(parentTransform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.SetActive(true);
        LoseHp(obj, damage);
        return obj;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj"></param>
    public void Recovery(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        if (loseHPPool.Contains(obj))
            return;
        loseHPPool.Enqueue(obj);
    }

    //掉血效果的实现
    public void LoseHp(GameObject obj, float damage)
    {
        // obj.transform.SetParent(m_hpNode);
        obj.GetComponent<Text>().text = "-" + 10;
        obj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        obj.transform.localRotation = Quaternion.identity;
        StartCoroutine(UpNumber(obj));//使实例的物体上升
    }

    /// <summary>
    /// 数字上升效果
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    IEnumerator UpNumber(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        obj.GetComponent<RectTransform>().DOLocalMove(new Vector3(0, 50f, 0), 1.0f);//用tween将数字移动到指定位置
        // obj.GetComponent<Renderer>().material.DOFade(0, 1);
        yield return new WaitForSeconds(1);//1秒钟后隐藏或者销毁
        Recovery(obj);
    }
}