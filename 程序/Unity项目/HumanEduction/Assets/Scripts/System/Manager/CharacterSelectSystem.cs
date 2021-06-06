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
            GameManager.Instance.currentUIManager.UIPanelDict[UIPanelType.LoginPanel].ToOtherPanel(UIPanelType.CharacterSelectPanel, Constants.SceneCharacter, GameProgress.CharacterSelect,true);
            print("名字空");
            return;
        }
        else
        {
            //直接加载主城场景
            //直接ChangetoOther
            print("即将进入主城");
            GameManager.Instance.currentUIManager.UIPanelDict[UIPanelType.LoginPanel].ToOtherPanel(UIPanelType.Ingame, Constants.SceneMainGame, GameProgress.InGame,true);
        }
    }

    //当收到名字请求回复的时候要做的事情：
    public void RspRename(NetMsg msg)
    {   
            GameManager.Instance.SetPlayerName(msg.rspRename.name);
            //跳转场景
            GameManager.Instance.currentUIManager.UIPanelDict[UIPanelType.CharacterSelectPanel].ToOtherPanel(UIPanelType.Ingame, Constants.SceneMainGame, GameProgress.InGame,true);
            //关闭当前页面
    }
}

