using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//当决定要进行chase的时候，要如何判定距离
public class ChaseNode : Node
{
    private Transform target; //玩家位置
    private NavMeshAgent agent; //导航系统
    private EnemyAI ai;

    public ChaseNode(Transform target, NavMeshAgent agent, EnemyAI ai)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.Chase(Color.yellow);
        float distance = Vector3.Distance(target.position, agent.transform.position);
        //为了防止距离玩家太近
        if (distance > 0.2f)
        {   
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            return NodeState.SUCCESS;
        }
    }


}