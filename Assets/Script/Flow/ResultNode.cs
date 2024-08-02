using UnityEngine;
using XNode;

public class ResultNode : BaseNode
{
    public new BaseNode nextNode
    {
        get { return null; }
        set { }
    }
    [Input] public string enter;

    [Output] public int endExit;
    [Output] public int loopExit;
    public bool condition; // true-endExit ; false-loopExit

    public string nodeDescription;

    public override void Execute()
    {
        Debug.Log(nodeDescription.ToString()+ " execute!");
    }

    public override BaseNode GetNextNode()
    {
        return null;
    }

    public BaseNode GetNextNode(bool isLoop)
    {
        NodePort nextPort = null;

        if (isLoop) {
            nextPort = GetOutputPort("endExit");
        } else {
            nextPort = GetOutputPort("loopExit");
        }
        return nextPort.Connection.node as BaseNode;
    }

    public override object GetValue(NodePort port)
    {
        string inputValue = GetInputValue<string>("enter", enter);
        if (port.fieldName == "endExit") {
            return condition ? inputValue : 0;
        } else if (port.fieldName == "loopExit") {
            return condition ? 0 : inputValue;
        }
        return null;
    }
}
