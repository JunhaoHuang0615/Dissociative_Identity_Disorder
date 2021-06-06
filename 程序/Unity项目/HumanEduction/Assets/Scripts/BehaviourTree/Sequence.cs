using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//他的节点必须全部都要完成才可以
public class Sequence : Node
{   
    //在规则树里，需要判定的节点
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }
    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;
        //评估每一个点是否达成条件
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    //某个节点的条件不满足，则整个Sequence就会不满足
                    //把这个Sequence直接设置成不满足
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        //是否是还正在进行评估的节点？ 如果没有了就把这个Sequence设置为成功
        _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _nodeState;
    }
}
