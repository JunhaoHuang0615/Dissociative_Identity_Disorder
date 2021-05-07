using System;
/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：所有游戏数据的管理者，各个系统的初始化
 *
 * Date:2021/5/5
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 *
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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


    // ======================  某些不需要贯穿整个游戏的系统的初始化 =============================
    public Dictionary<GameProgress,MonoBehaviour> progrressManagerDict;
    public GameProgress currentProgress;


    //UIManager相关
    [HideInInspector]
    private UIManager currentUIManager;

    //声音相关
    [HideInInspector]
    public AudioSourceManager audioManager;
    public AudioSource BGMAudioSource;
    public AudioSource gameeffectAudioSource;
    // public AudioClip[] audioClips;

    //信息，文字，语言相关
    [HideInInspector]
    public MessageManager messageManager;

    //一些系统的实例
    bool systemFound;
    public PaintFunction paintFunction;
    public LoginSystem loginSystem;

    //加载系统
    public AsyncOperation currentLoadScene;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        //=================================关于UIManager =====================================
        //加载资源包,加载完成资源包后，需要异步加载登录界面
        UIPackage.AddPackage("FGUI/Res_Main");
        UIPackage.AddPackage("FGUI/Res_Game");
        UIPackage.AddPackage("FGUI/Res_Component_public");
        //设置全局字体
        // UIConfig.defaultFont = "";

        //设置自适应
        GRoot.inst.SetContentScaleFactor(1920,1080,UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        //各个系统，并且按照顺序
        if(currentUIManager == null){
            currentUIManager = new UIManager();
            currentUIManager.OutGameMange();
        }

        // ================================================= 关于剧情 ==========================================

        if(messageManager == null){
            messageManager = new MessageManager();
        }

        // ====================================================== 关于声音 ============================================
        if(audioManager == null){
            audioManager = new AudioSourceManager(this);
            BGMAudioSource = GameObject.Find("BGMAudio").GetComponent<AudioSource>();
            gameeffectAudioSource = GameObject.Find("GameAudio").GetComponent<AudioSource>();

        }
        currentProgress = GameProgress.LoginSystem;

    }

    private void Update() {
         //什么时候加载这个系统？
        //Drawing系统初始化 ================================
        //根据玩家目前进入的进程来判断
        if(currentProgress == GameProgress.Drawing && systemFound == false){
            try{
                paintFunction = FindObjectOfType<PaintFunction>();
                paintFunction.getUIManager(currentUIManager);
                print("GameMananger找到paintFunction");
            }catch
            {
                print("重试获取paintMananger");
            }

            // if(progrressManagerDict.ContainsKey(currentProgress)==false){
            //     progrressManagerDict.Add(currentProgress,paintFunction);
            // }else{
            //     progrressManagerDict[currentProgress] = paintFunction;
            // }
        //疑问？？？
        //第二次加入时，paintFunction可能会被销毁,或者和当前存在于GameMananger的系统不同
            
        }
        //加载登录系统
         if(currentProgress == GameProgress.LoginSystem && systemFound == false){
             try{
                loginSystem = FindObjectOfType<LoginSystem>();
                loginSystem.getUIManager(currentUIManager);
                systemFound = true;
                print("找到Loginsystem");
             }
             catch{
                print("重试获取Loginsystem");
             }
            // if(progrressManagerDict.ContainsKey(currentProgress)==false){
            //     progrressManagerDict.Add(currentProgress,loginSystem);
            // }else{
            //     progrressManagerDict[currentProgress] = loginSystem;
            // }
            
        }



        //================= 关于loading界面的控制 ========================
        if(currentLoadScene!=null)
        currentUIManager.changeProgressBarValue(CommonGComp.LoadingProgressBar,currentLoadScene.progress*100);
        // if(currentLoadScene!=null)
        //     Debug.Log(GameManager.Instance.currentLoadScene.progress * 100);
        if(currentLoadScene!=null && currentLoadScene.progress == 1){
            currentUIManager.closeLoadingUI();
            currentLoadScene = null;
        }
    }

    //声音相关


    //控制场景加载,想去的场景，和要加载的进程
    public void AsyncLoadScene(string sceneName,GameProgress name){
        currentLoadScene = SceneManager.LoadSceneAsync(sceneName);
        currentProgress = name;
        systemFound = false;
    }

    //加载场景时，有的场景需要Loading界面过渡时可以使用这个方法
    public void LoadingToScene(string targetSceneName,GameProgress name){
        //loading的场景是哪一个
        AsyncLoadScene(targetSceneName,name);
        currentUIManager.loadingUI();
    }

}
