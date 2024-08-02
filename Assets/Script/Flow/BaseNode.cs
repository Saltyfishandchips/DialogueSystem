using XNode;

public abstract class BaseNode : Node
{
    [Output] public BaseNode nextNode;
    public bool isStartNode; // 标记是否是起始节点

    public abstract void Execute();
    public abstract BaseNode GetNextNode();
}
