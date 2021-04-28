using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{   
    private void Awake() {
        if(GameManager.Instance == null){
            //加载GameManager
            GameObject go = Resources.Load<GameObject>("Prefabs/System/GameManager");
            //实例化
            Instantiate(go,transform.position,transform.rotation);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
