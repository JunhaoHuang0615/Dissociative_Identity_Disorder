
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range; //判定范围
    private Transform target; //目标位置
    private Transform origin; //AI自己的位置

    public RangeNode(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}