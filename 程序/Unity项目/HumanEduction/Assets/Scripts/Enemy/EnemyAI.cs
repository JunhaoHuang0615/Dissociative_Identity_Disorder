using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//敌人组件需要挂上NavMeshAgent 导航系统
public class EnemyAI : MonoBehaviour
{   
    //怪物初始血量
    [SerializeField] private float startingHealth;
    //什么时候怪物被视为残血
    [SerializeField] private float lowHealthThreshold;
    //怪物回复血量的比例
    [SerializeField] private float healthRestoreRate;

    //怪物探索范围
    [SerializeField] private float chasingRange;
    //怪物攻击范围
    [SerializeField] private float shootingRange;
    //怪物移动速度
    [SerializeField] public float enemySpeed;
    //怪物家位置
    [SerializeField] public Transform homePosition;

    //怪物身上的rigbody组件
    [SerializeField] private Rigidbody rd;



    //玩家位置
    [SerializeField] private Transform playerTransform;
    //掩盖体
    [SerializeField] private Cover[] avaliableCovers;



    private Material material;
    private Transform bestCoverSpot;
    private NavMeshAgent agent;

    private Node topNode;

    private float _currentHealth;
    public float currentHealth
    {
        get { return _currentHealth; }
        //现在的血量，只能是再0~初始血量之间
        set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Start()
    {
        _currentHealth = startingHealth;
        ConstructBehahaviourTree();
    }

    //创造AI树
    private void ConstructBehahaviourTree()
    {   //注意添加的顺序，先后顺序是由程序员来定的
        IsCovereAvaliableNode coverAvaliableNode = new IsCovereAvaliableNode(avaliableCovers, playerTransform, this);
        GoToCoverNode goToCoverNode = new GoToCoverNode(agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold); //把这个AI的低血线传过去
        IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
        ChaseNode chaseNode = new ChaseNode(playerTransform, agent, this);
        RangeNode chasingRangeNode = new RangeNode(chasingRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent, this, playerTransform);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

        Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvaliableNode, goToCoverNode });
        Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
        Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
        Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });


    }

    private void Update()
    {
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.FAILURE)
        {
            //如果整棵树都是False ,要干嘛？
            idle(Color.red);
            agent.isStopped = true;
        }
        //敌人是会一直回复血量的，根据比例，脱战会快速回复满
        currentHealth += Time.deltaTime * healthRestoreRate;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }


    public void SetBestCoverSpot(Transform bestCoverSpot)
    {
        this.bestCoverSpot = bestCoverSpot;
    }

    public Transform GetBestCoverSpot()
    {
        return bestCoverSpot;
    }

    //怪物要进行隐藏时干嘛？

    public void GoToCover(Color color)
    {
        material.color = color;
        //移动方法已经写在了node里面了
    }

    //进入攻击范围后要干嘛？
    public void Shoot(Color color)
    {
        material.color = color;
    }
    //进入chase范围后要干嘛？
    public void Chase(Color color)
    {
        material.color = color;
    }
    public void idle(Color color)
    {
        material.color = color;
    }

}