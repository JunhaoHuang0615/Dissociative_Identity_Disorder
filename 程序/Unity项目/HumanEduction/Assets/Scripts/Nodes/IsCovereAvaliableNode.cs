using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCovereAvaliableNode : Node
{
    private Cover[] avaliableCovers;
    private Transform target;
    private EnemyAI ai;

    public IsCovereAvaliableNode(Cover[] avaliableCovers, Transform target, EnemyAI ai)
    {
        this.avaliableCovers = avaliableCovers;
        this.target = target;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {   
        //ai要去寻找最好的隐藏点
        Transform bestSpot = FindBestCoverSpot();
        ai.SetBestCoverSpot(bestSpot);
        //如果找不到最好的隐藏点，那么就不隐藏了
        return bestSpot != null ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private Transform FindBestCoverSpot()
    {
        if (ai.GetBestCoverSpot() != null)
        {
            if (CheckIfSpotIsValid(ai.GetBestCoverSpot()))
            {
                return ai.GetBestCoverSpot();
            }
        }
        float minAngle = 90; //cover物体的每90度方向检测一次
        Transform bestSpot = null;

        //便利所有可以使用的Cover
        for (int i = 0; i < avaliableCovers.Length; i++)
        {
            Transform bestSpotInCover = FindBestSpotInCover(avaliableCovers[i], ref minAngle);
            if (bestSpotInCover != null)
            {
                bestSpot = bestSpotInCover;
            }
        }
        return bestSpot;
    }
    // ref表示引用类型
    private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
    {
        Transform[] avaliableSpots = cover.GetCoverSpots();
        Transform bestSpot = null;
        for (int i = 0; i < avaliableSpots.Length; i++)
        {
            Vector3 direction = target.position - avaliableSpots[i].position;
            if (CheckIfSpotIsValid(avaliableSpots[i]))
            {
                float angle = Vector3.Angle(avaliableSpots[i].forward, direction);
                if (angle < minAngle)
                {
                    minAngle = angle;
                    bestSpot = avaliableSpots[i];
                }
            }
        }
        return bestSpot;
    }

    //判定是否可用
    private bool CheckIfSpotIsValid(Transform spot)
    {
        RaycastHit hit;
        Vector3 direction = target.position - spot.position;
        if (Physics.Raycast(spot.position, direction, out hit))
        {
            if (hit.collider.transform != target)
            {
                return true;
            }
        }
        return false;
    }
}