using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//前提条件
//1.给敌人增加Rigbody
//2.collider
public class EnemyTarget : MonoBehaviour
{   
    [SerializeField]
    Transform player; //target

    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody rigbody; //要给敌人体加力

    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
    }
}
