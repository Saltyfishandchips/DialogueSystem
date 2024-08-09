using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
 
public class ChatPanelManager : MonoBehaviour
{
    public GameObject leftBubblePrefab;
    public GameObject rightBubblePrefab;
 
    private ScrollRect scrollRect;
    private Scrollbar scrollbar;
    
    private RectTransform content;
 
    [SerializeField] 
    private float stepVertical; //上下两个气泡的垂直间隔
    [SerializeField] 
    private float stepHorizontal; //左右两个气泡的水平间隔
    [SerializeField]
    private float maxTextWidth;//文本内容的最大宽度

    [SerializeField] private float typeSpeed = 1f; //文本打字速度
 
    private float lastPos; //上一个气泡最下方的位置
    private float halfHeadLength;//头像高度的一半

    public void Init()
    {
        scrollRect = GetComponentInChildren<ScrollRect>();
        scrollbar = GetComponentInChildren<Scrollbar>();
        content = transform.Find("ViewPort").Find("Content").GetComponent<RectTransform>();
        lastPos = 0;
        halfHeadLength = leftBubblePrefab.transform.Find("head").GetComponent<RectTransform>().rect.height / 2;
    }
 
    public void AddBubble(string str, bool isMy)
    {
        GameObject newBubble = isMy ? Instantiate(rightBubblePrefab, this.content) : Instantiate(leftBubblePrefab, this.content);
        //设置气泡内容
        TextMeshProUGUI text = newBubble.GetComponentInChildren<TextMeshProUGUI>();

        ///增加打字机动画
        var t = DOTween.To(() => string.Empty, value => text.text = value, str, typeSpeed).SetEase(Ease.Linear);
        // //富文本
        t.SetOptions(true);

        text.text = str;
        if (text.preferredWidth > maxTextWidth)
        {
            text.GetComponent<LayoutElement>().preferredWidth = maxTextWidth;
        }
        //计算气泡的水平位置
        float hPos = isMy ? stepHorizontal / 2 : -stepHorizontal / 2;
        //计算气泡的垂直位置
        // float vPos = - stepVertical - halfHeadLength + lastPos;
        // float vPos = - stepVertical + lastPos;
        // newBubble.transform.localPosition = new Vector2(hPos, vPos);

        //更新lastPos
        Image bubbleImage = newBubble.GetComponentInChildren<Image>();
        float imageLength = GetContentSizeFitterPreferredSize(bubbleImage.GetComponent<RectTransform>(), bubbleImage.GetComponent<ContentSizeFitter>()).y;

        // float vPos = - stepVertical / 2 - imageLength/ 2 + lastPos;
        float vPos = - imageLength / 2 - stepVertical / 2 + lastPos;
        newBubble.transform.localPosition = new Vector2(hPos, vPos);

        lastPos = vPos - imageLength / 2 - stepVertical / 2;
        //更新content的长度
        if (-lastPos > content.rect.height)
        {
            this.content.sizeDelta = new Vector2(this.content.rect.width, -lastPos);
        }
 
        scrollRect.verticalNormalizedPosition = 0;//使滑动条滚轮在最下方
    }
 
    public Vector2 GetContentSizeFitterPreferredSize(RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        return new Vector2(HandleSelfFittingAlongAxis(0, rect, contentSizeFitter),
            HandleSelfFittingAlongAxis(1, rect, contentSizeFitter));
    }
 
    private float HandleSelfFittingAlongAxis(int axis, RectTransform rect, ContentSizeFitter contentSizeFitter)
    {
        ContentSizeFitter.FitMode fitting =
            (axis == 0 ? contentSizeFitter.horizontalFit : contentSizeFitter.verticalFit);
        if (fitting == ContentSizeFitter.FitMode.MinSize)
        {
            return LayoutUtility.GetMinSize(rect, axis);
        }
        else
        {
            return LayoutUtility.GetPreferredSize(rect, axis);
        }
    }
}