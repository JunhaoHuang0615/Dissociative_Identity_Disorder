﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class GameManager : MonoBehaviour
{   
    //负责整个游戏的运行逻辑
    //GameManager要使用单例
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    //UIManager相关
    [HideInInspector]
    public GameProgress currentProgress;
    private UIManager currentUIManager;

    //声音相关
    [HideInInspector]
    public AudioSourceManager audioManager;
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    //信息，文字，语言相关
    [HideInInspector]
    public MessageManager messageManager;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        //=================================关于UIManager =====================================
        //加载资源包
        UIPackage.AddPackage("FGUI/Res_Main");
        UIPackage.AddPackage("FGUI/Res_Game");
        UIPackage.AddPackage("FGUI/Res_Component_public");
        //设置全局字体
        // UIConfig.defaultFont = "";

        //设置自适应
        GRoot.inst.SetContentScaleFactor(1920,1080,UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        //实例UIManager
        if(currentUIManager == null){
            currentUIManager = new UIManager();
            currentUIManager.OutGameMange();
        }
        if(audioManager == null){
            audioManager = new AudioSourceManager(this);
        }
        if(messageManager == null){
            messageManager = new MessageManager();
        }

        // ==================================关于
    }

    //声音相关
    
}
