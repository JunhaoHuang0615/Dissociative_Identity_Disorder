using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Protocol;

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

    private Character_Linked_File character_Linked_File; //从GameMananger里面拿，GameMananger可以从其他地方拿

    private GButton btn_returnmenu;
    private GButton btn_enterGame;
    private GTextInput nameInput;
    protected override void OnInitPanel(){
        //动画可能后期不需要
        Transition t = panelMask.GetTransition("hide_mask");
        t.Play();
        chaImgComp = UIPackage.CreateObject("Panel_Character_Select","character_display").asCom;
        chaInfoComp = UIPackage.CreateObject("Panel_Character_Select","character_info").asCom;
        uIManager.commonComp.Add(CommonGComp.ChaImgComp,chaImgComp);
        uIManager.commonComp.Add(CommonGComp.ChaInfoComp,chaInfoComp);
        nameInput = contentPane.GetChild("NameInput").asTextInput;
        uIManager.commonInputText.Add(CommonGComp.NameTextInput, nameInput);

        btn_returnmenu = contentPane.GetChild("btn_mainMenu").asButton;
        btn_returnmenu.onClick.Add(returnMainMenu);

        btn_enterGame = contentPane.GetChild("btn_enterGame").asButton;
        btn_enterGame.onClick.Add(sendNameToServer);
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

    public void displayInfo(){
        contentPane.AddChild(chaInfoComp);
        Transition t = chaImgComp.GetTransition("enter");
        t.Play();
    }

    //注册事件，如按钮，需要传递有参数的方法时，需要用到lamada表达式
    public void displayImg(GComponent targetComp){
        contentPane.AddChild(targetComp);
        Transition t = targetComp.GetTransition("t0");
        t.Play();
    }
    //在注册按钮事件时，需要使用：例如组件targetComp中有一个按钮btn1，给他注册事件
    //targetComp.GetChild("btn1").onClick.Add(  ()=>{displayImg(chuandiComp);}    )
    //下线以后才可以切换至主界面
    private void returnMainMenu(){
        ToOtherPanel(UIPanelType.LoginPanel,Constants.SceneLogin,GameProgress.LoginSystem);
    }

    //这里需要向服务器发送名字，并且得到回复后才可以进入下一个场景，此方法离线模式可用

    private void enterMainGame(){
        ToOtherPanel(UIPanelType.Ingame,Constants.SceneMainGame,GameProgress.InGame);
    }

    private void sendNameToServer()
    {
        if (nameInput.text == "")
        {
            uIManager.showTipsWindow("请输入名称");
        }
        else
        {
            NetMsg netMsg = new NetMsg
            {
                cmd = (int)CMD.ReqRename,
                reqRename = new ReqRename
                {
                    name = nameInput.text
                }
            };
            NetSvc.Instance.SendMsg(netMsg);
        }
    }
}
