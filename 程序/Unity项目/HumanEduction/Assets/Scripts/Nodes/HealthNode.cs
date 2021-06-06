using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private EnemyAI ai;
    private float threshold; //入口，阈值，临界值，用于做对比的health

    public HealthNode(EnemyAI ai, float threshold)
    {
        this.ai = ai;
        this.threshold = threshold;
    }

    public override NodeState Evaluate()
    {   
        //如何评估？评估怪物的血量
        return ai.currentHealth <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
        //不会存在返回RunningState的情况
    }
}