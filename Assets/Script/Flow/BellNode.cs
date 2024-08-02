using UnityEngine;
using XNode;

public class BellNode : BaseNode
{
    [Input] public string enter;

    public string nodeDescription;

    public override void Execute()
    {   
        FlowDataManager.Instance.updateCurrentNPC();
        FlowDataManager.Instance.updateCurrentTravelPermit();
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
