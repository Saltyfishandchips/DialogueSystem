using UnityEngine;
using XNode;

public class ExaminationNode : BaseNode
{
    [Input] public string enter;

    public string nodeDescription;
    public override void Execute()
    {
        Debug.Log(nodeDescription.ToString() + " execute!");
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override BaseNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as BaseNode;
    }
}
