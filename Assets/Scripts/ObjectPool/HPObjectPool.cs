using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Y9g;

public class HPObjectPool : Singleton<HPObjectPool>
{
    private Queue<GameObject> hpPool = new Queue<GameObject>();
    public Queue<GameObject> HPPool { get { return hpPool; } }
    [SerializeField]
    private GameObject HPPrefab; // 血量的预制体
    private const int Default_Expand_Count = 10; // 默认扩展数量

    void Start()
    {
        ExpandLoseHP(HPPrefab);
    }

    /// <summary>
    /// 扩展对象池
    /// </summary>
    /// <param name="count"></param>
    /// <param name="prefab"></param>
    private void ExpandLoseHP(GameObject prefab, int count = Default_Expand_Count)
    {
        GameObject obj = null;
        for (int index = 0; index < count; index++)
        {
            obj = Instantiate(prefab);
            obj.transform.SetParent(transform);
            obj.SetActive(false);
            hpPool.Enqueue(obj);
        }
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public GameObject GetObject(Transform parentTransform)
    {
        if (hpPool.Count <= 0)
            ExpandLoseHP(HPPrefab);
        GameObject obj = hpPool.Dequeue();
        obj.transform.SetParent(parentTransform);
        obj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        obj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        obj.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj"></param>
    private void Recovery(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        if (hpPool.Contains(obj))
            return;
        hpPool.Enqueue(obj);
    }

    // 扣血效果的实现
    public void LoseHp(GameObject obj, float damage)
    {
        obj.GetComponent<Text>().text = "-" + damage;
        obj.GetComponent<Text>().color = Color.red;
        StartCoroutine(UpNumber(obj)); // 使实例的物体上升
    }

    // 回血效果的实现
    public void RecoverHp(GameObject obj, float damage)
    {
        obj.GetComponent<Text>().text = "+" + damage;
        obj.GetComponent<Text>().color = Color.green;
        StartCoroutine(UpNumber(obj)); // 使实例的物体上升
    }

    /// <summary>
    /// 数字上升效果
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    IEnumerator UpNumber(GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 pos = obj.GetComponent<RectTransform>().localPosition;
        obj.GetComponent<RectTransform>().DOLocalMove(new Vector3(pos.x, pos.y + 50f, pos.z), 1.0f);
        yield return new WaitForSeconds(1); // 1秒钟后回收对象
        Recovery(obj);
    }
}