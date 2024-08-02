using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private DialogueTrigger inkTrigger;
    [SerializeField] private AnimEventTrigger animEventTrigger;


    private void Awake() {
        

        if (!DayManager.hasIntoHome) {
            DayManager.hasIntoHome = true;
        }
        else {
            animEventTrigger.hasFirstDialogue = false;
        }   
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {
        if (DayManager.dayPass > 0) {
            //更换张子虚或者其他NPC，Ink文件，之后需要修改代码，融入存档系统
            //TODO:修改这里
            inkTrigger.inkJosn = Resources.Load<TextAsset>("Dialogues/guijiebao_daily/gjb_zzxrc1");
        } 
    }
}
