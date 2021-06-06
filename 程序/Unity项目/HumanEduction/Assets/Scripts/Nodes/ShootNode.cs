using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyAI ai;
    private Transform target;

    private Vector3 currentVelocity;
    private float smoothDamp;

    public ShootNode(NavMeshAgent agent, EnemyAI ai, Transform target)
    {
        this.agent = agent;
        this.ai = ai;
        this.target = target;
        smoothDamp = 1f;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;
        //这个就是敌人要进行的动作，这里只是改变了敌人的颜色表示要发动攻击了，方法是写在敌人的类里的
        ai.Shoot(Color.green); 

        Vector3 direction = target.position - ai.transform.position;
        Vector3 currentDirection = Vector3.SmoothDamp(ai.transform.forward, direction, ref currentVelocity, smoothDamp);
        Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
        ai.transform.rotation = rotation;
        //如果是RunningState的时候，下次检测会碰到RunningState,并且检测他是否可以Success了
        return NodeState.RUNNING;
    }

}