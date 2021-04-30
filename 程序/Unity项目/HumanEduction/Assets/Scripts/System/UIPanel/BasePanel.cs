using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class BasePanel : Window
{   
    //包名称
    protected string packageName;

    //动画
    protected Transition transition;

    protected Controller controller;
    protected GComponent panelMask;//大部分UI可能会有个遮罩
    protected UIPanelType currentUIPanel; //当前游戏显示的UI类型
    protected  UIManager uIManager;

    public BasePanel(string packageName,UIPanelType uIPanelType, UIManager uIManager){
        this.packageName = packageName;
        this.currentUIPanel = uIPanelType;
        this.uIManager = uIManager;
        UIPackage.AddPackage("FGUI/"+packageName);

    }

    protected override void OnInit()
    {
        //执行共有部分
        //Window 下的contentPane就是当前包中最重要的组件
        contentPane = UIPackage.CreateObject(packageName,"Main").asCom;// 前提是这个包有Main组件
        panelMask = this.contentPane.GetChild("PanelMask").asCom; //前提是必须都有
        //执行特有部分
        OnInitPanel();
    }

    protected virtual void OnInitPanel(){
        //子类重写
    }

    //因为每一个页面都有设置一个Mask，那么他们的入场都可以相同
    //由UIManager来调用
    public void EnterPanel(){
        Transition t = panelMask.GetTransition("hide_mask");
        t.Play();

    }

    //进入其他页面
    protected void ToOtherPanel(UIPanelType otherType){ //传递想要跳转的UIPanel过来
        ExitPanel(()=>{
            ChangePanelCallBack(otherType);
        });
    }
    //退出页面
    protected void ExitPanel(PlayCompleteCallback callback){
        Transition t = panelMask.GetTransition("show_mask");
        t.Play(callback);//这个callback就是在Play方法执行完之后，要执行的方法 方法可以是lanmada表达式
        //如果不使用回调函数，可能会造成动画没有播放完毕
    }

    protected void ChangePanelCallBack(UIPanelType otherType){
        uIManager.UIPanelDict[currentUIPanel].Hide();
        uIManager.UIPanelDict[otherType].Show();
        uIManager.UIPanelDict[otherType].EnterPanel();


        
    }
}
