using UnityEngine;
using UnityEngine.UI;

public class ToggleInkTrigger : MonoBehaviour
{
    [SerializeField] private Toggle wrongToggle;
    [SerializeField] private Toggle rightToggle;
    public string inkName;
    public int choice;

    private void Start() {
        // wrongToggle.isOn = false;
        // rightToggle.isOn = false;
        rightToggle.onValueChanged.AddListener((bool value) => {
            if (value) {
                rightToggle.isOn = true;
                wrongToggle.isOn = false;
            }
        });

        wrongToggle.onValueChanged.AddListener((bool value) => {
            if (value) {
                wrongToggle.isOn = true;
                rightToggle.isOn = false;
                DialogueManager.Instance.CacheInkFile(inkName, choice);
            }
        });
    }
}
