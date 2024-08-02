using UnityEngine;

public class SwingObject : MonoBehaviour
{
     public float swingAngleLeft = 30f;  // 向左摆动的最大角度
    public float swingAngleRight = 60f;  // 向右摆动的最大角度
    public float swingSpeed = 30.0f;   // 摆动的速度
    private bool isSwinging = false;

    void Update()
    {
        // Update 可以保持为空或者用于其他功能
    }

    public void StartSwinging()
    {
        if (!isSwinging)
        {
            isSwinging = true;
            StartCoroutine(SwingRoutine());
        }
    }

    System.Collections.IEnumerator SwingRoutine()
    {
        // 向左摆动 30°
        yield return StartCoroutine(SwingToAngle(-swingAngleLeft));
        
        // 向右摆动 60°
        yield return StartCoroutine(SwingToAngle(swingAngleRight));
        
        // 再向左摆动 30°
        yield return StartCoroutine(SwingToAngle(-swingAngleLeft));

        // 摆动结束，回到初始位置
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        isSwinging = false;
    }

    System.Collections.IEnumerator SwingToAngle(float targetAngle)
    {
        float startAngle = transform.localEulerAngles.z;
        if (startAngle > 180) startAngle -= 360; // 将角度调整到 -180 到 180 之间

        float elapsedTime = 0;
        float duration = Mathf.Abs(targetAngle - startAngle) / swingSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float angle = Mathf.Lerp(startAngle, targetAngle, elapsedTime / duration);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        transform.localRotation = Quaternion.Euler(0, 0, targetAngle);
    }

    public void OnMouseDown()
    {
        StartSwinging();
    }
}
