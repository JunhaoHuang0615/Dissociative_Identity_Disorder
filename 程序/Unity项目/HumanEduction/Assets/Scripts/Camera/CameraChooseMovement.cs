using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChooseMovement : MonoBehaviour
{
    public GameObject offsetViewPoint;
    public float transitionSpeed=5; //相机在两个之间选择时，移动的快慢
    private Vector3 offsetPos;
    private Vector3 targetPos;

    private void Awake() {
        offsetPos = offsetViewPoint.transform.position;
        targetPos = offsetPos;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){ //0 左键 1 右键 2 中
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //摄像机向鼠标为发射一条射线
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,1000)){ //1000码距离之内碰到的物体，判断是否有碰到物体 
                if(hit.collider.tag == "Player"){
                    targetPos = hit.collider.GetComponent<CharacterLinkCamPos>().linkedCamPos.transform.position;
                }
            }

        }
        if(Input.GetKeyDown(KeyCode.X)){
            targetPos = offsetPos;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //首先去offset的位置
        this.transform.position = Vector3.Lerp(this.transform.position
                        ,targetPos,Time.deltaTime * transitionSpeed);
    }
}
