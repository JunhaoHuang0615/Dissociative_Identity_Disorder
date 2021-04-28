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
    // private void Awake() {
    //     GRoot.inst.SetContentScaleFactor(1920,1080);
    //     // GRoot.inst.MakeFullScreen();
    //     UIPackage.AddPackage("FGUI/LoginGUI");
    //     GComponent component = UIPackage.CreateObject("LoginGUI","Login_UI_Panel").asCom;
    //     GRoot.inst.AddChild(component);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
