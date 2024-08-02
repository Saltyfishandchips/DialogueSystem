using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class DealWithTakeOut : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private RectTransform originPanelRectTransform;
    private RectTransform transferPanelRectTransform;

    private Vector4 boundary;

    void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("MainCanvas").GetComponentInParent<Canvas>();

        originPanelRectTransform = GameObject.Find("SubmitPanel").GetComponent<RectTransform>();
        transferPanelRectTransform = GameObject.Find("OperationPanel").GetComponent<RectTransform>();

        // 动态添加 CanvasGroup 组件
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        boundary = getIntersection(originPanelRectTransform,transferPanelRectTransform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 获取当前物体的兄弟索引
        int currentIndex = rectTransform.GetSiblingIndex();

        // 将物体移动到最后，确保它在最前面显示
        rectTransform.SetSiblingIndex(canvas.transform.childCount - 1);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // 改变透明度
        canvasGroup.blocksRaycasts = false; // 禁用射线检测，使拖动时不被遮挡
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 获取鼠标位置并转换为Canvas内的本地坐标
        Vector2 pointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out pointerPosition);

        // 设置UI元素的位置为限制后的世界坐标
        rectTransform.localPosition = clampInPanel(pointerPosition,boundary);

        // 检测 UI 元素所处位置
        if(RectTransformUtility.RectangleContainsScreenPoint(originPanelRectTransform, eventData.position, eventData.pressEventCamera))
        {
            //
        }
        else if(RectTransformUtility.RectangleContainsScreenPoint(transferPanelRectTransform, eventData.position, eventData.pressEventCamera))
        {
            if(FlowDataManager.Instance.currentNPC.Settings.specialEvent == NpcSpecialEvent.receiveTakeout)
            {
                //给出外卖
                Destroy(gameObject);
            }
            else
            {
                float dealt = transferPanelRectTransform.localPosition.y - transferPanelRectTransform.rect.height/2;
                rectTransform.localPosition = new Vector3(0.0f, dealt - 5.0f , 0.0f);
            }
            //给出外卖
            //Destroy(gameObject);

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f; // 恢复透明度
        canvasGroup.blocksRaycasts = true; // 恢复射线检测
    }


    private Vector4 getIntersection(RectTransform rectTransform1,RectTransform rectTransform2)
    {
        Vector4 res = Vector4.zero; // minx，miny,maxx,maxy

        // 获取 Panel 四个顶点的世界坐标
        Vector3[] worldCorners1 = new Vector3[4];
        rectTransform1.GetWorldCorners(worldCorners1);

        Vector3[] worldCorners2 = new Vector3[4];
        rectTransform2.GetWorldCorners(worldCorners2);

        // 将世界坐标转换为 Canvas 的本地坐标
        Vector3[] localCorners1 = new Vector3[4];
        Vector3[] localCorners2 = new Vector3[4];
        for (int i = 0; i < worldCorners1.Length; i++)
        {
            localCorners1[i] = canvas.transform.InverseTransformPoint(worldCorners1[i]);
            localCorners2[i] = canvas.transform.InverseTransformPoint(worldCorners2[i]);
        }

        res.x = Mathf.Min(localCorners1[0].x,localCorners2[0].x);
        res.y = Mathf.Min(localCorners1[0].y,localCorners2[0].y);
        res.z = Mathf.Max(localCorners1[2].x,localCorners2[2].x);
        res.w = Mathf.Max(localCorners1[2].y,localCorners2[2].y);

        return res;
    }

    private Vector2 clampInPanel(Vector2 position, Vector4 boundary)
    {
        float eps = 3.0f;
        // 限制拖动区域在 Panel 内部
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(position.x, boundary.x+eps, boundary.z-eps),
            Mathf.Clamp(position.y, boundary.y+eps, boundary.w-eps)
        );

        return clampedPosition;
    }
}
