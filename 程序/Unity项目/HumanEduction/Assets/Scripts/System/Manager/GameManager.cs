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

    //Drawing系统
    private bool isInDrawingScene;
    public PaintFunction paintFunction;

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
        //初始化各个系统，并且按照顺序
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

        isInDrawingScene = false;

    }

    private void Update() {
         //什么时候加载这个系统？
        //先暂时直接拿到UIManager，之后是要判断才能将这个实例化
        if(isInDrawingScene == true){
            paintFunction = FindObjectOfType<PaintFunction>();
            if(paintFunction == null){
                print("GameMananger找不到paintFunction");
            }
            paintFunction.getUIManager(currentUIManager);
        }

    }

    //声音相关


    //控制场景加载
    public void AsyncLoadScene(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
    }
    
}
