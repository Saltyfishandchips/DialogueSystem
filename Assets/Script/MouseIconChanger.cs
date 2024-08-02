using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MouseIconChanger : MonoBehaviour, IPointerClickHandler {
    public Texture2D customCursor; // 自定义鼠标图标
    public Texture2D defaultCursor; // 默认鼠标图标
    private bool isCustomCursorActive = false; // 标识当前鼠标图标状态

    [SerializeField] private Sprite youbi;
    [SerializeField] private Sprite nobi;

    void Start() 
    {
        // 获取默认鼠标图标
        defaultCursor = CursorTextureFromCurrent();
    }

    // 当点击Image对象时调用
    public void OnPointerClick(PointerEventData eventData) 
    {
        if (isCustomCursorActive) {
            // 还原默认鼠标图标
            this.GetComponent<Image>().sprite = youbi;
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

            AudioManager.Instance.PlaySFX("BrushDropdown");
            //Debug.Log(isCustomCursorActive.ToString());
        }
        else {
            // 更改为自定义鼠标图标
            this.GetComponent<Image>().sprite = nobi;
            // Vector2 cursorHotspot =  new Vector2(0,customCursor.Size().y);
            Vector2 cursorHotspot =  new Vector2(0, 305);
    
            Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.ForceSoftware);

            AudioManager.Instance.PlaySFX("BrushPickup");
            //Debug.Log(isCustomCursorActive.ToString());
        }

        // 切换鼠标图标状态
        isCustomCursorActive = !isCustomCursorActive;
        
        FlowDataManager.Instance.isWrittingBrushOn = isCustomCursorActive;
        // if( !isCustomCursorActive  && (IsPointerInsidePanel(eventData,guiYinPanelRectTransform) ||
        //     IsPointerInsidePanel(eventData,fanYangPanelRectTransform) ||
        //     IsPointerInsidePanel(eventData,transferPanelRectTransform)) )
        // {
        //     // ExaminationEnd state.
        //     Debug.Log("ExaminationEnd state");
        // }
    }

    // private bool IsPointerInsidePanel(PointerEventData eventData ,RectTransform panelRectTransform) 
    // {
    //     Vector2 localMousePosition = panelRectTransform.InverseTransformPoint(eventData.position);
    //     Debug.Log(panelRectTransform.rect.Contains(localMousePosition).ToString());
    //     return panelRectTransform.rect.Contains(localMousePosition);
    // }

    // 获取当前默认鼠标图标（这里假设初始鼠标图标为空）
    private Texture2D CursorTextureFromCurrent() {
        // 在实际项目中，你可能需要通过其他方式获取默认鼠标图标
        return defaultCursor;
    }

    public void ResetMouseIcon()
    {
        this.GetComponent<Image>().sprite = youbi;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}

