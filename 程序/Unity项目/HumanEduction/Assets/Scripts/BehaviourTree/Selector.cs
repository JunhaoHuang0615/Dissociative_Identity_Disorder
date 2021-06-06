using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//他的节点只要有一个标识完成，则就直接完成
public class Selector : Node
{
    protected List<Node> nodes = new List<Node>();

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                case NodeState.SUCCESS:
                    //这个Selector是成功的，直接返回
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                case NodeState.FAILURE:
                    //去下一个子节点
                    break;
                default:
                    break;
            }
        }
        //到最后了没有正在运行或者成功的，则说明这个Selector是失败的了
        _nodeState = NodeState.FAILURE;
        return _nodeState;
    }
}