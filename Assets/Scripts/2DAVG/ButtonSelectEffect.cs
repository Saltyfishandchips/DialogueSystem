using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f;
    private Button button;
    private Vector3 initialScale;
    // Start is called before the first frame update

    private void Awake() {
        button = GetComponent<Button>();
    
    }

    void Start()
    {
        initialScale = transform.localScale;
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = initialScale * scaleFactor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = initialScale;
    }
}
