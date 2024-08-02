using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class AppearNode : BaseNode
{
    [Input] public string enter;

    public string nodeDescription;

    public override void Execute()
    {
        //对话
        GameObject root = GameObject.Find("MainCanvas");
        
        Transform DialogueCanvas = root.transform.Find("DialogueCanvas");
        DialogueCanvas.gameObject.SetActive(true);

        int currentIndex = DialogueCanvas.GetSiblingIndex();
        // 将物体移动到最后，确保它在最前面显示
        DialogueCanvas.SetSiblingIndex(root.transform.childCount - 1);

        string str = FlowDataManager.Instance.currentNPC.Settings.preDialogues;

        TextAsset textAsset = Resources.Load("Dialogues/pre/" + str) as TextAsset;
        DialogueManager.Instance.InitializedStroy(textAsset,0);
        
        //人物
        GameObject img = GameObject.Find("SoulImage");
        string iconPath = FlowDataManager.Instance.currentNPC.Settings.npcIcon;
        Texture2D temp =  Resources.Load<Texture2D>("CharacterSprite/NpcSprite/" + iconPath);
        Sprite characterSprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));
        AudioManager.Instance.PlaySFX("GhostShowup");

        img.GetComponent<Image>().sprite = characterSprite;
        
        img.GetComponent<Image>().color = new Color(255.0f,255.0f,255.0f,100.0f);
        Debug.Log(nodeDescription.ToString()+ " execute!");
    }

    public override BaseNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as BaseNode;
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}
