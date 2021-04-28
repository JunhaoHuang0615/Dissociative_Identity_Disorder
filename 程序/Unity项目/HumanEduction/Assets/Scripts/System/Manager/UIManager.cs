using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        UIPanelDict.Add(UIPanelType.LoginPanel,new FGUI_Login("Panel_LoginGUI",UIPanelType.LoginPanel,this));
        UIPanelDict.Add(UIPanelType.Loading,new FGUI_loading("Panel_Loading",UIPanelType.Loading,this));
        UIPanelDict.Add(UIPanelType.SettingPanel,new FGUI_SettingPanel("Panel_Setting",UIPanelType.SettingPanel,this));
        UIPanelDict.Add(UIPanelType.CharacterSelectPanel,new FGUI_Character_Select("Panel_Character_Select",
                    UIPanelType.CharacterSelectPanel,this));
        //我们游戏初始化首先第一个见面就是Login
        UIPanelDict[UIPanelType.LoginPanel].Show();
    }

}
