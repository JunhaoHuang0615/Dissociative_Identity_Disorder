using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{   
    private GameObject target;
    private Vector3 offset;

    [Range(0.01f,1.0f)] //0.16会让相机延迟到达
    public float smoothFactor = 0.5f;

    public bool lookAtPlayer; //是否让相机始终与目标角度一致
    private void Awake() {
        //相机跟随目标
        target = GameObject.FindWithTag("Player");
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // this.transform.position = offset + 
        //     new Vector3(target.transform.position.x,-4,target.transform.position.z);
        //     // Y轴不会发生改变
        Vector3 newPos = target.transform.position + offset;
        this.transform.position = Vector3.Slerp(this.transform.position,newPos,smoothFactor);
        if(lookAtPlayer){
            this.transform.LookAt(target.transform);

        }

    }
}
