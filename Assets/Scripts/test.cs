using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test : MonoBehaviour
{
    [SerializeField] Button button_1;
    [SerializeField] Button button_2;
    [SerializeField] Button button_3;

    private void Awake() {
        button_1.onClick.AddListener(() => {

            DialogueManager.Instance.CacheInkFile("D1_4_preDialogues", 0);
        });

        button_2.onClick.AddListener(() => {
            DialogueManager.Instance.CacheInkFile("D2_8_preDialogues", 0);
        });

        button_3.onClick.AddListener(() => {
            DialogueManager.Instance.CacheInkFile("D2_6_preDialogues", 0);
        });
    }

    void Update()
    {


        if (Input.GetKeyUp(KeyCode.A))
        {
            //DialogueManager.Instance.InitializedStroy(textAsset, 0);
        }
    }
}
 
 