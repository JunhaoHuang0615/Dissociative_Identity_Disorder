/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：玩家的登录界面系统管理，由GameMananger初始化
 *      只写关于System的东西，数据获取要通过GameMananger来得到
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

public class LoginSystem : MonoBehaviour
{
    private UIManager uIManager;
    [HideInInspector]
    public string accountNumber;
    [HideInInspector]
    public string passwordNumber;


    public void getUIManager(UIManager uIManager){
        this.uIManager = uIManager;
    }

    public void Work() {
        //检查玩家电脑本地是否已经含有了账号了密码的数据
        if(PlayerPrefs.HasKey("Acct")&&PlayerPrefs.HasKey("Pass")){
            accountNumber = PlayerPrefs.GetString("Acct");
            passwordNumber = PlayerPrefs.GetString("Pass");
        }
        else{
            accountNumber = "";
            passwordNumber = "";
        }
        GameManager.Instance.audioManager.ChangeBGM(BGMType.bgmtype1);
                
    }

    private void Update() {
        //Todo:
        //记录玩家第一次输入的账号密码

        //控制UI

    }

    public bool checkUsernameEmpty(){
        bool isempty;
        if(uIManager.commonInputText[CommonGComp.AccountTextInput].text == "" || uIManager.commonInputText[CommonGComp.AccountTextInput].text==null){
            uIManager.tipsText.text  = "账号不能为空";
            isempty = true;
        }else{
            isempty = false;
            //Perhaps:
            //GameManager去拿数据，由数据来更新
            accountNumber= uIManager.commonInputText[CommonGComp.AccountTextInput].text;
            passwordNumber = uIManager.commonInputText[CommonGComp.PasswordTextInput].text;
        }
        return isempty;
    }
    //向服务器发送请求
    public void ConnectToNetServer()
    {
        NetMsg message = new NetMsg {
            //必须是类里面的成员
            cmd = (int)CMD.ReqLogin,
            reqLogin = new ReqLogin
            {
                account = accountNumber,
                password = passwordNumber
            }
        };
        NetSvc.Instance.SendMsg(message);
    }

    public void HandleServerData(NetMsg netMsg)
    {
        GameManager.Instance.SetPlayerData(netMsg.rspLogin);
        GameManager.Instance.currentUIManager.UIPanelDict[UIPanelType.LoginPanel].ToOtherPanel(UIPanelType.CharacterSelectPanel, true, Constants.SceneCharacter, GameProgress.CharacterSelect);
        if(GameManager.Instance.characterSelectSystem == null)
        {
            print("null");
        }
        GameManager.Instance.characterSelectSystem.HandlePlayerData(netMsg);
    }
}
