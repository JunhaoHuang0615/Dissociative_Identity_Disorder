using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_Login : BasePanel
{   
    public FGUI_Login(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {

    }
    protected override void OnInitPanel(){
        //拿到此组件自己身上的动画
        transition = contentPane.GetTransition("WhiteMaskPanel");
        transition.Play();
        //还要执行遮罩消失的动画，如果有的话,因为在第一次登入的时候，是没有执行EnterPanel这个方法的
        //Enterpanel这个方法只有在切换的时候使用
        Transition t = panelMask.GetTransition("hide_mask");
        t.Play();
        }
}
