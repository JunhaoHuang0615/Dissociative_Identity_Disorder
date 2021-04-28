using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_Character_Select : BasePanel
{   
    public FGUI_Character_Select(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {

    }
    //要获取某个角色的图片以及文本

    //获得组件
    private GComponent chaImgComp;
    private GComponent chaInfoComp;

    private Character_Linked_File character_Linked_File;
 

    private void Awake() {
        // //Main组件可以直接显示（角色选择窗口）
        // GRoot.inst.SetContentScaleFactor(1920,1080);
        // // GRoot.inst.MakeFullScreen();
        // UIPackage.AddPackage("FGUI/Character_Select");
        // //创建UIPanel ， 也可以把这个总的UICOMPONENE当作属性
        // GComponent component = UIPackage.CreateObject("Character_Select","Main").asCom;
        // GRoot.inst.AddChild(component);

        // chaImgComp = UIPackage.CreateObject("Character_Select","character_display").asCom;
        // chaInfoComp = UIPackage.CreateObject("Character_Select","character_info").asCom;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)){ //0 左键 1 右键 2 中
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //摄像机向鼠标为发射一条射线
        //     RaycastHit hit;
        //     if(Physics.Raycast(ray, out hit,1000)){ //1000码距离之内碰到的物体，判断是否有碰到物体 
        //         if(hit.collider.tag == "Player"){
        //             character_Linked_File = hit.collider.GetComponent<Character_Linked_File>();
        //             chaInfoComp.GetChild("character_description").asTextField.text = character_Linked_File.text;
        //             // chaImgComp.GetChild("n3").asLoader.SetSize(559,678);
        //             chaImgComp.GetChild("n3").asLoader.url = character_Linked_File.linkedImgurl;
        //             displayImg();
        //             displayInfo();
        //         }
        //     }


        // }
        // if(Input.GetKeyDown(KeyCode.X)){
        //     GRoot.inst.RemoveChild(chaImgComp);
        //     GRoot.inst.RemoveChild(chaInfoComp);
        // }
    }

    void displayImg(){
        GRoot.inst.AddChild(chaImgComp);
        Transition t = chaImgComp.GetTransition("t0");
        t.Play();
    }

    void displayInfo(){
        GRoot.inst.AddChild(chaInfoComp);
    }

    //注册事件，如按钮，需要传递有参数的方法时，需要用到lamada表达式
    // void displayImg(GComponent targetComp){
    //     GRoot.inst.AddChild(targetComp);
    //     Transition t = targetComp.GetTransition("t0");
    //     t.Play();
    // }
    //在注册按钮事件时，需要使用：例如组件targetComp中有一个按钮btn1，给他注册事件
    //targetComp.GetChild("btn1").onClick.Add(  ()=>{displayImg(chuandiComp);}    )
}
