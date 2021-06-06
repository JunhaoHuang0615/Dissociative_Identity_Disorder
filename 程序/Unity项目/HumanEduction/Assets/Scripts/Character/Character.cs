using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{   
    private GameObject player;
    float playerMoveSpeed = 3f; //角色移动速度
    // ====================== Jump  跳跃系统参数======================
    //跳跃需要获得刚体组件
    private float playerJumpHigh = 10f;
    private float playerJumpHighCon = 0.35f;
    public LayerMask groundLayer; //地面检测使用
    private CapsuleCollider chaCollider; //角色刚体
    Rigidbody rigbody;
    //跳跃按键时间计算
    private float jumpTimeCounter;
    private float jumpTime = 0.2f; //可以设置的按键时长
    private bool isJumping;

    //==========================================================
    //定义两个方向的参数h和v
    float h; // 水平方向（Horizontal）通过U3D的设置，A和D， 左和右将会产生水平方向的变化
    float v; // 纵深方向

    void Awake() {
        player = this.gameObject; //找到游戏中Tag是Player的OBJ
        rigbody = this.GetComponent<Rigidbody>();
        chaCollider = this.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        playerMoveMethod(h,v);

        if(isGround() && Input.GetKeyDown(KeyCode.Space)){
            playerJumpMethod();
        }
        if(isJumping == true && Input.GetKey(KeyCode.Space)){
            playerHighJump();
        }
        // 提起按键，则直接无法继续蓄力跳跃
        if(Input.GetKeyUp(KeyCode.Space)){
            isJumping = false;
        }
    }
    //由于跳跃关系到物理，因此需要使用fixupdate
    private void FixedUpdate() {

    }
    // ================================= 玩家移动 ===================================
    void playerMoveMethod(float h , float v){
        //水平方向 并按照世界坐标
        player.transform.Translate(Vector3.right * h *playerMoveSpeed * Time.deltaTime, Space.World);
        //纵深方向  transform.translate(Vector3.forward，Space.Self) 
        player.transform.Translate(Vector3.forward * v *playerMoveSpeed * Time.deltaTime, Space.World);
    }
    // ================================  玩家跳跃  =================================

    void playerJumpMethod(){
        //重新请求跳跃
        isJumping = true;
        jumpTimeCounter  = jumpTime;
        //直接让角色在Y轴有一个速度
        rigbody.AddForce(Vector3.up*playerJumpHigh,ForceMode.Impulse);
        // rigbody.velocity += Vector3.up*playerJumpHigh;
    }

    //大小跳实现方法
    void playerHighJump(){
        //如果蓄力跳跃的时长还存在，并且还是按下space的状态
        if(jumpTimeCounter > 0){
            rigbody.AddForce(Vector3.up*playerJumpHighCon,ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;
        } else{
        //如果时间已经到了，不能蓄力了，那么就直接把跳跃过程关了
            isJumping = false;

        }
    }
    //地面检测
    bool isGround(){
        bool isHit = Physics.CheckCapsule(chaCollider.bounds.center,new Vector3(chaCollider.bounds.center.x,
        chaCollider.bounds.min.y,chaCollider.center.z),chaCollider.radius * .9f,groundLayer);

        return isHit;

    }
    //获得按键输入

    public float getH(){
        return h;
    }
    public float getV(){
        return v;
    }
}
