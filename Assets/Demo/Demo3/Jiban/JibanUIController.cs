using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JibanUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject JibanTop;
    [SerializeField]
    private GameObject JibanCenter;
    [SerializeField]
    private GameObject JibanBottom;

    private float JibanTopHeight;
    private float JibanCenterHeight;
    private float JibanBottomHeight;

    void Start()
    {
        JibanTopHeight = JibanTop.GetComponent<RectTransform>().rect.height;
        JibanCenterHeight = JibanCenter.GetComponent<RectTransform>().rect.height;
        JibanBottomHeight = JibanBottom.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetJibanCenterHeight(100);

            AdjustJibanTopPosition();
            AdjustJibanBottomPosition();
        }
    }

    private void SetJibanCenterHeight(float height)
    {
        Vector2 JibanCenterSize = JibanCenter.GetComponent<RectTransform>().sizeDelta;
        JibanCenterSize.y = height;
        JibanCenter.GetComponent<RectTransform>().sizeDelta = JibanCenterSize;
        JibanCenterHeight = JibanCenter.GetComponent<RectTransform>().rect.height;
        Debug.Log(JibanCenterHeight);
    }

    private void AdjustJibanTopPosition()
    {
        Vector3 JibanTopPosition = JibanTop.GetComponent<RectTransform>().anchoredPosition;
        JibanTopPosition.y = JibanCenterHeight + JibanTopHeight;
        JibanTop.GetComponent<RectTransform>().anchoredPosition = JibanTopPosition;
    }

    private void AdjustJibanBottomPosition()
    {
        Vector3 JibanBottomPosition = JibanBottom.GetComponent<RectTransform>().anchoredPosition;
        JibanBottomPosition.y = -JibanCenterHeight - JibanBottomHeight;
        JibanBottom.GetComponent<RectTransform>().anchoredPosition = JibanBottomPosition;
    }
}