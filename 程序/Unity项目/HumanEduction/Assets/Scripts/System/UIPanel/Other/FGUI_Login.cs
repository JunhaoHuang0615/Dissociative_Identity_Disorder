using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_Login : BasePanel
{   
    private GButton btn_start;
    private GTextInput accountTextBox;
    private GTextInput passwordTextBox;
    public FGUI_Login(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {

    }
    protected override void OnInitPanel(){
        GameManager.Instance.LoadingToScene(Constants.SceneLogin,GameProgress.CharacterSelect);
        //拿到此组件自己身上的动画
        transition = contentPane.GetTransition("WhiteMaskPanel");
        transition.Play();
        //还要执行遮罩消失的动画，如果有的话,因为在第一次登入的时候，是没有执行EnterPanel这个方法的
        //Enterpanel这个方法只有在切换的时候使用
        Transition t = panelMask.GetTransition("hide_mask");
        //开始按钮
        btn_start = contentPane.GetChild("btn_start").asButton;
        btn_start.onClick.Add(()=>{

            if(GameManager.Instance.loginSystem.checkUsernameEmpty()==true){
                uIManager.tipsWindow.showTipsPanel();
            }else{
                GameManager.Instance.loginSystem.ConnectToNetServer();
            }

        });
        t.Play();

        //在UIMananger中注册
        accountTextBox = contentPane.GetChild("accountinput").asTextInput;
        passwordTextBox = contentPane.GetChild("passwordinput").asTextInput;
        uIManager.commonInputText.Add(CommonGComp.AccountTextInput,accountTextBox);
        uIManager.commonInputText.Add(CommonGComp.PasswordTextInput,passwordTextBox);
        // accountTextBox.text = GameManager.Instance.loginSystem.accountNumber;
        // passwordTextBox.text = GameManager.Instance.loginSystem.passwordNumber;
        }


}
