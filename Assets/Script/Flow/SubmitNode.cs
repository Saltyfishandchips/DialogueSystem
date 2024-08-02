using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SubmitNode : BaseNode
{
    [Input] public string enter;

    public string nodeDescription;

    public override void Execute()
    {
        
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
