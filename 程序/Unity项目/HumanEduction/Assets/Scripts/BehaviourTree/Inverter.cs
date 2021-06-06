using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{       
    //装饰器
    //用于把某个节点的成功与失败直接反转
    protected Node node;

    public Inverter(Node node)
    {
        this.node = node;
    }
    public override NodeState Evaluate()
    {
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;

                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        return _nodeState;
    }
}