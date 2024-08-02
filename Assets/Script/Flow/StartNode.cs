using XNode;
using UnityEngine;

public class StartNode : BaseNode
{

    public string nodeDescription;

    public override void Execute()
    {   
        FlowDataManager.Instance.updateCurrentDaily();
        FlowDataManager.Instance.updateObituary();
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
