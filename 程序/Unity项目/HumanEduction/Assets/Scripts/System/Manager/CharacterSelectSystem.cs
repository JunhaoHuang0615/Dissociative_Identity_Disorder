/***
 *
 * Title:"" 项目：人格离析
 * 主题：，由GameMananger管理
 * Description:
 * 功能：橘色选择系统
 *
 * Date:2021/05/07
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

public class CharacterSelectSystem : MonoBehaviour
{   
    public void Work()
    {

    }
    //TODO检测玩家是否已经创建了角色
    public void HandlePlayerData(NetMsg msg)
    {
        if(msg.rspLogin.playerData.name == "")
        {
           //创建角色界面
           
        }
        else
        {
           //直接加载主城场景
           //直接ChangetoOther
        }
    }
}

