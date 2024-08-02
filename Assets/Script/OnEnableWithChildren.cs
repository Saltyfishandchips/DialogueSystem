using UnityEngine;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;

public class OnEnableWithChildren : MonoBehaviour
{
    public RectTransform rectTransform;

    public TMP_Text[] othersTexts;
    void OnEnable()
    {
        setFalse();
        // 当物体被激活时，将列表中的物体设置为子物体
        SetChildren();
    }

    void SetChildren()
    {
        switch(FlowDataManager.Instance.obituaryTotal.currentPage)
        {
            case 1:
                foreach(var go in  FlowDataManager.Instance.firstPageGOs )
                {
                    go.SetActive(true);
                }
                break;
        
            case 2:
                foreach(var go in  FlowDataManager.Instance.cataloguePageGos )
                {
                    go.SetActive(true);
                }
                break;

            default:
                foreach(var go in  othersTexts)
                {
                    go.gameObject.SetActive(true);
                }
                break;
        }

        Debug.Log("Child objects have been set as children of " + gameObject.name);
    }

    void setFalse()
    {
        foreach (RectTransform rect in rectTransform)
        {
            if (rect != null)
            {
                rect.gameObject.SetActive(false);
            }
        }
    }
}
