using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
    public Image backgroundImage;  // 引用到 Image 组件
    public Sprite[] frames;  // 存储所有帧的数组
    public float framesPerSecond = 10.0f;  // 每秒播放的帧数
    private int currentFrame;
    private float timer;
    private bool isPlaying = false;

    // 烟雾效果
    public RectTransform leftImage;  // 左侧图片的 RectTransform
    public RectTransform rightImage;  // 右侧图片的 RectTransform
    public float moveDuration = 1.0f;  // 移动持续时间
    public Vector2 centerOffset = new Vector2(1000, 0);  // 图片靠拢时的偏移量
    private Vector2 leftInitialPos;
    private Vector2 rightInitialPos;
    private bool isMoving = false;


    // 日历和铃铛
    public GameObject object1;  // 第一个对象
    public GameObject object2;  // 第二个对象
    public float moveDistance = 2.0f;  // 移动距离

    private Vector3 object1InitialPos;
    private Vector3 object2InitialPos;

    void Start()
    {
        if (backgroundImage == null)
        {
            Debug.LogError("Background Image component is not assigned.");
            return;
        }

        if (frames.Length == 0)
        {
            Debug.LogError("No frames assigned to the animation.");
            return;
        }

        // 初始化动画参数
        currentFrame = 0;
        timer = 0.0f;

        //
        leftInitialPos = leftImage.anchoredPosition;
        rightInitialPos = rightImage.anchoredPosition;

        object1InitialPos = object1.transform.position;
        object2InitialPos = object2.transform.position;
    }

    void Update()
    {
        if (isPlaying)
        {
            // 更新计时器
            timer += Time.deltaTime;

            // 计算当前帧的索引
            int frameIndex = (int)(timer * framesPerSecond);

            // 如果帧索引发生变化，更新 Image 的 sprite 属性
            if (frameIndex != currentFrame && frameIndex < frames.Length)
            {
                currentFrame = frameIndex;
                backgroundImage.sprite = frames[currentFrame];
            }

            // 动画播放完成，停止播放并停留在最后一帧
            if (frameIndex >= frames.Length)
            {
                isPlaying = false;
                backgroundImage.sprite = frames[frames.Length - 1];
                StartMoving();
            }
        }
    }

    // 开始播放动画
    public void PlayAnimation()
    {
        isPlaying = true;
        timer = 0.0f;
        currentFrame = 0;
        backgroundImage.sprite = frames[currentFrame];
    }

     void StartMoving()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToCenterAndBack());
            StartCoroutine(MoveObjectsRoutine());
        }
    }

    System.Collections.IEnumerator MoveToCenterAndBack()
    {
        isMoving = true;

        // 计算中心位置
        Vector2 leftTargetPos = leftInitialPos + centerOffset;
        Vector2 rightTargetPos = rightInitialPos - centerOffset;

        // 向中心移动
        yield return StartCoroutine(MoveImagesRoutine(leftImage, rightImage, leftTargetPos, rightTargetPos, moveDuration));

        // 从中心移回初始位置
        yield return StartCoroutine(MoveImagesRoutine(leftImage, rightImage, leftInitialPos, rightInitialPos, moveDuration));

        isMoving = false;
    }

    System.Collections.IEnumerator MoveImagesRoutine(RectTransform left, RectTransform right, Vector2 leftTarget, Vector2 rightTarget, float duration)
    {
        float elapsedTime = 0;

        Vector2 leftStartPos = left.anchoredPosition;
        Vector2 rightStartPos = right.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            left.anchoredPosition = Vector2.Lerp(leftStartPos, leftTarget, t);
            right.anchoredPosition = Vector2.Lerp(rightStartPos, rightTarget, t);

            yield return null;
        }

        left.anchoredPosition = leftTarget;
        right.anchoredPosition = rightTarget;
    }

    System.Collections.IEnumerator MoveObjectsRoutine()
    {
        isMoving = true;
        float elapsedTime = 0;

        Vector3 object1TargetPos = object1InitialPos - new Vector3(0, moveDistance, 0);
        Vector3 object2TargetPos = object2InitialPos - new Vector3(0, moveDistance, 0);

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            object1.transform.position = Vector3.Lerp(object1InitialPos, object1TargetPos, t);
            object2.transform.position = Vector3.Lerp(object2InitialPos, object2TargetPos, t);

            yield return null;
        }

        object1.transform.position = object1TargetPos;
        object2.transform.position = object2TargetPos;

        isMoving = false;
    }
}
