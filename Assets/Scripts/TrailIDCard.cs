using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TrailIDCard : MonoBehaviour
{
    [SerializeField] private Button fingerPrintButton;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float durationTime;
    [SerializeField] private float scaleFactor;
    private bool canMove = false;
    private Vector3 originScale;
    private Vector3 largerScale;

    private bool isOver = false;

    private void Awake() {
        fingerPrintButton.onClick.AddListener(() => {
            canMove = true;
        });
        originScale = transform.localScale;
        largerScale = transform.localScale * scaleFactor;
    }

    private void Update() {
        if (canMove) {
            // Vector3.MoveTowards(gameObject.transform.localPosition, targetPos, speed*Time.deltaTime);
            transform.DOMove(targetPos, durationTime);
        }
        RaycastCheck();

        if (isOver)
        {
            Vector2 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//转换成世界坐标
            transform.position = new Vector2(Pos.x, Pos.y);
        }
    }

    private void RaycastCheck() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);

        // RaycastHit2D hit = Physics2D.Raycast(screenPos, new Vector3(screenPos.x, screenPos.y, screenPos.z - 100));
        RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);
        ScaleChange(hit);

    }

    private void ScaleChange(RaycastHit2D hit2D) {
        
        if (hit2D && hit2D.transform.tag == "Fengduwendie") {
            
            transform.localScale = largerScale;
        }
        else {
            transform.localScale = originScale;
        }
        
    }
}
