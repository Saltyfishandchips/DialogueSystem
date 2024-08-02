using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveImagesToCenter : MonoBehaviour
{
    public Image leftImage;  // 左边的 Image 组件
    public Image rightImage;  // 右边的 Image 组件
    public float duration = 1f;  // 移动持续时间

    public float moveDistance = 500f;  // 移动距离

    private RectTransform leftRectTransform;
    private RectTransform rightRectTransform;
    private Vector3 leftStartPos;
    private Vector3 rightStartPos;
    private Vector3 leftTargetPos;
    private Vector3 rightTargetPos;

    void Start()
    {
        // 获取 RectTransform 组件
        leftRectTransform = leftImage.GetComponent<RectTransform>();
        rightRectTransform = rightImage.GetComponent<RectTransform>();

        // 记录初始位置
        leftStartPos = leftRectTransform.anchoredPosition;
        rightStartPos = rightRectTransform.anchoredPosition;

        // 计算目标位置
        leftTargetPos = leftStartPos + new Vector3(moveDistance, 0, 0);
        rightTargetPos = rightStartPos - new Vector3(moveDistance, 0, 0);
        // 开始协程
        //StartCoroutine(MoveImages());
    }

    public void startMove()
    {
        StartCoroutine(MoveImages());
    }

    IEnumerator MoveImages()
    {
        leftImage.GetComponent<RectTransform>().SetAsLastSibling();
        rightImage.GetComponent<RectTransform>().SetAsLastSibling();

                float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // 线性插值移动
            leftRectTransform.anchoredPosition = Vector3.Lerp(leftStartPos, leftTargetPos, t);
            rightRectTransform.anchoredPosition = Vector3.Lerp(rightStartPos, rightTargetPos, t);

            yield return null;
        }

        // 确保最终位置
        leftRectTransform.anchoredPosition = leftTargetPos;
        rightRectTransform.anchoredPosition = rightTargetPos;
        Debug.Log("Images have moved to the center.");
    }
}
