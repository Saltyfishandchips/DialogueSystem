using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChaner : MonoBehaviour
{
    public Texture2D defaultCursor; // 默认鼠标图标
    public FlowManager flowManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        if(flowManagerInstance && flowManagerInstance.isDayOver == true)
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
