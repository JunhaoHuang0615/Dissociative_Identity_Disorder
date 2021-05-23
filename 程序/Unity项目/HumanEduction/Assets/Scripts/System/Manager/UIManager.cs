/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：UIManager是所有UI的总管理，其他地方要介入UI时，需要通过它
        它由GameManager管理，由它初始化
        加载资源包，外部需要调用的方法
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 *
 *
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIManager
{
    public GameProgress gameProgress;
    public Dictionary<UIPanelType,BasePanel> UIPanelDict;
    public UIPanelType currentUIPanel;

    public Dictionary<CommonGComp,GProgressBar> commonBar;
    public Dictionary<CommonGComp,GComponent> commonComp;

    public Dictionary<CommonGComp,GTextInput> commonInputText;

    //关于提示的窗口
    public FGUI_TipsPanel tipsWindow;
    public GTextField tipsText;

    public UIManager(){
        UIPanelDict = new Dictionary<UIPanelType, BasePanel>();
        commonBar = new Dictionary<CommonGComp, GProgressBar>();
        commonComp = new Dictionary<CommonGComp, GComponent>();
        commonInputText = new Dictionary<CommonGComp, GTextInput>();
    }
    //清空字典
    public void ClearDict(){
        //当切换场景时，需要把之前的场景的对象清空
        foreach(var item in UIPanelDict){
            item.Value.Dispose();
        }

        UIPanelDict.Clear();

    }

    //在游戏里时的管理
    public void InGameUIMange(){
        gameProgress = GameProgress.InGame;

    }

    //在Login界面时的管理
    public void OutGameMange(){
        gameProgress = GameProgress.OutGame;

        if(UIPanelDict.Count!=0){
            //如果Dict里面有东西，我们要清空一下
            ClearDict();
        }
        //注册所有的UIPanel
        tipsWindow = new FGUI_TipsPanel(this);
        UIPanelDict.Add(UIPanelType.Loading,new FGUI_loading("Panel_Loading",UIPanelType.Loading,this));
        UIPanelDict.Add(UIPanelType.LoginPanel,new FGUI_Login("Panel_LoginGUI",UIPanelType.LoginPanel,this));
        UIPanelDict.Add(UIPanelType.SettingPanel,new FGUI_SettingPanel("Panel_Setting",UIPanelType.SettingPanel,this));
        UIPanelDict.Add(UIPanelType.CharacterSelectPanel,new FGUI_Character_Select("Panel_Character_Select",
                    UIPanelType.CharacterSelectPanel,this));
        UIPanelDict.Add(UIPanelType.Drawing,new FGUI_DrawingPanel("Panel_Drawing",
                    UIPanelType.Drawing,this));
        UIPanelDict.Add(UIPanelType.Colorize,new FGUI_Colorize("Panel_Colorize",
                    UIPanelType.Colorize,this));   
        //我们游戏初始化首先第一个见面就是Login
        UIPanelDict[UIPanelType.LoginPanel].Show();
        
        
    }

    //检测是否点击在了UI上
    public bool isOnFGUI(){
        return Stage.isTouchOnUI;
    }

    //播放Loading的方法,很多场景载入之前会用到
    //还要传入场景UI的载入百分比
    public void loadingUI(){
        Debug.Log("Loading界面出来了");
        UIPanelDict[UIPanelType.Loading].Show();
    }
    public void closeLoadingUI(){
        UIPanelDict[UIPanelType.Loading].Hide();
    }

    //更改Bar类型参数值
    public void changeProgressBarValue(CommonGComp bar, double value){
        commonBar[bar].value = value;
    }

    //得到InputText类型参数
    public string getInputTextValue(CommonGComp inputTextComp){
        return commonInputText[inputTextComp].text;
    }

    //某个动画播放完毕之后要做的事情，如等待某个动画的时间长度，放完之后要做的事情
    public IEnumerator AniPlayDone(float sec, Action cb){
        yield return new WaitForSeconds(sec);
        if(cb!=null){
            cb();
        }
    }
    // ====================== Tips Window Method ======================================
    public void showTipsWindow(string text)
    {
        tipsText.text = text;
        tipsWindow.showTipsPanel();
    }
    public void showTipsWindow()
    {
        tipsWindow.showTipsPanel();
    }
    public void changeTipsText(string text)
    {
        tipsText.text = text;
    }

    public void tipsWindowFront()
    {
        tipsWindow.BringToFront();
    }
    

}
