using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{   
    private GameObject target;
    private Vector3 offset;
    private void Awake() {
        target = GameObject.FindWithTag("Player");
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = offset + target.transform.position;
    }
}
