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
using Protocol;

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
/*    public Dictionary<GameProgress,MonoBehaviour> progrressManagerDict;*/
    public GameProgress currentProgress;
    //一些系统的实例
    public PaintFunction paintFunction;
    public LoginSystem loginSystem;
    public CharacterSelectSystem characterSelectSystem;
    //Enhance：可以设置一个总类，这个class继承monobehavior，然后其他系统继承它，这样只需要去注册新系统，加入总系统列表
    //初始化的时候，所有系统都会被初始化

    // ============================================================================================

    // ===========UIManager相关
    //[HideInInspector]
    public UIManager currentUIManager;
    public bool shouldShowWindow = false;

    // ===========声音相关
    [HideInInspector]
    public AudioSourceManager audioManager;
    public AudioSource BGMAudioSource;
    public AudioSource gameeffectAudioSource;
    // public AudioClip[] audioClips;

    // ==========信息，文字，语言相关
    [HideInInspector]
    public MessageManager messageManager;

    // ==========加载系统
    public AsyncOperation currentLoadScene;

    // ==========玩家数据
    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }

    public void SetPlayerData(RspLogin data)
    {
        playerData = data.playerData;
    }
    public void SetPlayerName(string name)
    {
        playerData.name = name;
    }
    //UI Loading
    public bool needload = false;

    // =========================================================================================
    // =========================================================================================
    // =========================================================================================


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

        // ================================================= 开始
        currentProgress = GameProgress.LoginSystem;




        // =================================================== 协议及服务 =================================================================
        NetSvc net = this.GetComponent<NetSvc>();
        net.InitSec();

        


    }

    private void Update() {
        //什么时候加载这个系统？
        //Drawing系统初始化 ================================
        if(characterSelectSystem == null)
        {
            characterSelectSystem = this.GetComponent<CharacterSelectSystem>();
        }
        if (loginSystem == null)
        {
            loginSystem = this.GetComponent<LoginSystem>();
            loginSystem.getUIManager(currentUIManager);
        }
        //根据玩家目前进入的进程来判断,后面直接挂到GameManger上
        if (currentProgress == GameProgress.Drawing ) {
            try {
                paintFunction = FindObjectOfType<PaintFunction>();
                paintFunction.getUIManager(currentUIManager);
                print("GameMananger找到paintFunction");
            } catch
            {
                print("重试获取paintMananger");
            }

        }
        //加载登录系统
        if (currentProgress == GameProgress.LoginSystem)
        {
            loginSystem.Work();
        }
            
            //加载角色创建系统
        if (currentProgress == GameProgress.CharacterSelect)
        {
            characterSelectSystem.Work();
        }


        //================= 关于loading界面的控制 ========================
        if (currentLoadScene != null && needload)
            currentUIManager.changeProgressBarValue(CommonGComp.LoadingProgressBar, currentLoadScene.progress * 100);
        // if(currentLoadScene!=null)
        //     Debug.Log(GameManager.Instance.currentLoadScene.progress * 100);
        if (currentLoadScene != null && currentLoadScene.progress == 1 && needload) {
            currentUIManager.closeLoadingUI();
            print("关闭Loading");
            currentLoadScene = null;
            needload = false;
        }


        //================= 控制TipsWindow的开启（服务器传输使用） ================
        if (shouldShowWindow)
        {
            currentUIManager.showTipsWindow();
            shouldShowWindow = false;
        }
        if (currentUIManager.tipsWindow.isPanelShowing)
        {
            currentUIManager.tipsWindowFront();
        }
    }
    //声音相关
    //控制场景加载,想去的场景，和要加载的进程
    public void AsyncLoadScene(string sceneName, GameProgress name)
    {
        currentLoadScene = SceneManager.LoadSceneAsync(sceneName);
        currentProgress = name;
    }

    //加载场景时，有的场景需要Loading界面过渡时可以使用这个方法
    public void LoadingToScene(string targetSceneName, GameProgress name)
    {
        needload = true;
        //loading的场景是哪一个
        AsyncLoadScene(targetSceneName, name);
        currentUIManager.loadingUI();
    }
}
