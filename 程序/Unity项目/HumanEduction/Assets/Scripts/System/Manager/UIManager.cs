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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class UIManager
{
    public GameProgress gameProgress;
    public Dictionary<UIPanelType,BasePanel> UIPanelDict;
    public UIPanelType currentUIPanel;

    public UIManager(){
        UIPanelDict = new Dictionary<UIPanelType, BasePanel>();
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
        //UIPanelDict[UIPanelType.LoginPanel].Show();
        //UIPanelDict[UIPanelType.LoginPanel].Show();
    }

    //检测是否点击在了UI上
    public bool isOnFGUI(){
        return Stage.isTouchOnUI;
    }

    

}
