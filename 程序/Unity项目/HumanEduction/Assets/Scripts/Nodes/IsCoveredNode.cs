using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCoveredNode : Node
{
    private Transform target;
    private Transform origin;

    public IsCoveredNode(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        RaycastHit hit;
        //从自己(origin)的位置发射一个范围的射线
        if (Physics.Raycast(origin.position, target.position - origin.position, out hit))
        {
            if (hit.collider.transform != target) //如果撞到的不是目标，则说明是一个cover物体
            {
                return NodeState.SUCCESS;
            }
        }
        return NodeState.FAILURE;
    }
}