using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_loading : BasePanel
{   
    public FGUI_loading(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {

    }
    // //获得进度条
    // private GProgressBar progressBar;
    // private GComponent mainPic;
    
    // private void Awake() {
    //     generateLoading();
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // void generateLoading(){
    //     GRoot.inst.SetContentScaleFactor(1920,1080);
    //     UIPackage.AddPackage("FGUI/Loading");
    //     mainPic = UIPackage.CreateObject("Loading","loading_comp").asCom;
    //     GRoot.inst.AddChild(mainPic);
    //     progressBar = mainPic.GetChild("n0").asProgress;
    //     progressBar.TweenValue(100,5); //5秒之内加载到100 ，这里的5可以是int类型的其他值
    // }
}
