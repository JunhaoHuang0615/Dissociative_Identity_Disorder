/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：当某些操作错误，或者出血其他的状况是，需要显示的帮助提示
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_TipsPanel : Window
{   
    GTextField tipsText;
    UIManager uIManager; 
    Transition enterAnim;
    public bool isPanelShowing = false;

    public FGUI_TipsPanel(UIManager uIManager){
        this.uIManager = uIManager;
        //添加包
        UIPackage.AddPackage("FGUI/"+"Panel_Tips");
        contentPane = UIPackage.CreateObject("Panel_Tips","Main").asCom;
        tipsText = contentPane.GetChild("text").asTextField;
        uIManager.tipsText = tipsText;
        enterAnim = contentPane.GetTransition("enter");
    }
    protected override void OnInit()
    {
        //还有动画，以及tips的窗口队列

    }
    //TODO队列显示
    
    //
    public void showTipsPanel(){
        if(!isPanelShowing)
        {
            isPanelShowing = true;
            this.Show();
            enterAnim.Play(()=>{
                this.Hide();
                isPanelShowing = false;
            }
            );
        }
    }
}
