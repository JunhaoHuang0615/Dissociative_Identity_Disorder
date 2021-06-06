using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景物体挂载脚本
//每一个隐藏点都是某个基本物体的子物体（空位置，可命名为spot）
//每一个场景物体都要是Static的
//之后用Navigation面板，bake所有的路径
public class Cover : MonoBehaviour
{   
    [SerializeField] private Transform[] coverSpots;
    //每一个Cover都要设置几个隐藏点

    public Transform[] GetCoverSpots()
    {
        return coverSpots;

    }
}