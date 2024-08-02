using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ZZX;
    [SerializeField] private AnimEventTrigger animEventTrigger; 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject niutou;
    [SerializeField] private GameObject dailyNPC;
    [SerializeField] private GameObject yidianExit;
    [SerializeField] private GameObject newBornPostion;

    // 小糖人
    [SerializeField] private DialogueTrigger ttManInkJosn;

    private void Awake() {
        if (DayManager.dayPass == 0 && DayManager.hasIntoMarket) { // 未存档，从鬼宅进入鬼市
            animEventTrigger.hasFirstDialogue = false;

            foreach (var zzx in ZZX) {
                zzx.SetActive(false);
            }
            // niutou.SetActive(true);
            player.transform.position = newBornPostion.transform.position;
            yidianExit.SetActive(false);
            niutou.SetActive(false);
        }
        else if (DayManager.dayPass > 0 && DayManager.hasIntoMarket) { // 存档后，从鬼宅进入鬼市
            foreach (var zzx in ZZX) {
                zzx.SetActive(false);
            }
            niutou.SetActive(true);
            yidianExit.SetActive(true);
            player.transform.position = newBornPostion.transform.position;
            ttManInkJosn.inkJosn = Resources.Load<TextAsset>("Dialogues/guijiebao_daily/gjb_ttm2");
        }
        else if (DayManager.dayPass == 0 && !DayManager.hasIntoMarket) { // 初次进入鬼市
            DayManager.hasIntoMarket = true;
            yidianExit.SetActive(false);
            niutou.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
