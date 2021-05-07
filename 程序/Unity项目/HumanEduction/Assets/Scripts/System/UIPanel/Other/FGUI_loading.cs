using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_loading : BasePanel
{   
    //获得进度条
    private GProgressBar progressBar;

    public FGUI_loading(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {
        
    }

    protected override void OnInitPanel(){
        Transition t = panelMask.GetTransition("hide_mask");
        t.Play();
        
        progressBar = contentPane.GetChild("LoadingProgressBar").asProgress;
        uIManager.commonBar.Add(CommonGComp.LoadingProgressBar,progressBar);
        //因为progress是1为加载完成
        progressBar.value = GameManager.Instance.currentLoadScene.progress * 100;
    
    }
    
    // private void Awake() {
    //     generateLoading();
    // }

    // Update is called once per frame

    // void generateLoading(){
    //     GRoot.inst.SetContentScaleFactor(1920,1080);
    //     UIPackage.AddPackage("FGUI/Loading");
    //     mainPic = UIPackage.CreateObject("Loading","loading_comp").asCom;
    //     GRoot.inst.AddChild(mainPic);
    //     progressBar = mainPic.GetChild("n0").asProgress;
    //     progressBar.TweenValue(100,5); //5秒之内加载到100 ，这里的5可以是int类型的其他值
    // }

    //loading根据要加载的场景来显示的动画
}
