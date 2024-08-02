using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 跟随的目标物体
    public float damping = 1; // 跟随的平滑程度

    public float cameraHeight = 5;
    public float minCameraSize = 3;
    public float cameraDamping = 1; // 缩放的平滑程度
    private Camera mainCamera;

    public Vector3 offSet;
    
    private void Awake() {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置在屏幕上的坐标
            Vector3 targetPosition = Camera.main.WorldToViewportPoint(target.position);
            // 计算摄像机应该移动的向量
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, targetPosition.z));
            // 计算摄像机的目标位置
            Vector3 destination = transform.position + delta;
            // 相机高度
            destination.y += cameraHeight;

            destination += offSet;
            // 使用平滑阻尼移动摄像机
            transform.position = Vector3.Lerp(transform.position, destination, damping * Time.deltaTime);
            
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minCameraSize, cameraDamping * Time.deltaTime);
        }
    }
    

}
