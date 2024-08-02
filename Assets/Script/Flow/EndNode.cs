using XNode;
using UnityEngine;

public class EndNode : BaseNode
{
    public new BaseNode nextNode
    {
        get { return null; }
        set { }
    }
    
    [Input] public string enter;

    public string nodeDescription;

    public override void Execute()
    {   
        Debug.Log(nodeDescription.ToString()+ " execute!");
    }

    public override BaseNode GetNextNode()
    {
        return null;
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}
