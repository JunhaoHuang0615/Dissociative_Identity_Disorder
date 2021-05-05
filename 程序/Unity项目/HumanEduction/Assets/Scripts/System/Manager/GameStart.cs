/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：游戏的启动，制造GameMananger
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 *
 *
 */

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
}
