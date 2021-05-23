/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：网络服务
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;
using PENet;

public class NetSvc : MonoBehaviour
{
    public static NetSvc Instance = null;
    PENet.PESocket<ClientSession, NetMsg> client = null;
    private Queue<NetMsg> msgQue = new Queue<NetMsg>();
    private static readonly string obj = "lock";
    public void InitSec()
    {
        Instance = this;
        client = new PENet.PESocket<ClientSession, NetMsg>();
        client.SetLog(true, (string msg, int lv) => {

            switch (lv)
            {
                case 0: //当日志等级是0的时候
                    msg = "Log" + msg;
                    Debug.Log(msg);
                    break;
                case 1: //当日志等级是0的时候
                    msg = "Warn" + msg;
                    Debug.Log(msg);
                    break;
                case 2: //当日志等级是0的时候
                    msg = "Error" + msg;
                    Debug.Log(msg);
                    break;
                case 3: //当日志等级是0的时候
                    msg = "Info" + msg;
                    Debug.Log(msg);
                    break;

            }

        });
        client.StartAsClient(IPConfig.srvIP, IPConfig.srvport);

        Debug.Log("Network Service Connected");
    }

    //登录发送协议
    public void SendMsg(NetMsg netMsg)
    {
        if(client.session != null)
        {
            client.session.SendMsg(netMsg);
        }
        else
        {
            //显示一个提示框
            GameManager.Instance.currentUIManager.showTipsWindow("无法连接服务器");
            InitSec();

        }
    }

    public void AddNetMsg(NetMsg msg)
    {
        lock (obj)
        {
            msgQue.Enqueue(msg);
        }
    }
    private void Update()
    {
        if(msgQue.Count > 0)
        {
            lock (obj)
            {
                //客户端解包
                print("解包");
                NetMsg msg = msgQue.Dequeue();
                ProcessMessage(msg);
            }
        }
    }

    
    //根据包的内容来决定是哪一个系统处理包
    private void ProcessMessage(NetMsg msg)
    {
        //要发送的东西时有意义的
        if (msg.err != (int)ErrCode.None)
        {
            print(msg.err);
            switch (((ErrCode)msg.err))
            {
                case ErrCode.AcctIsOnline:
                    GameManager.Instance.currentUIManager.showTipsWindow("账号已经在线");
                    break;
                case ErrCode.WrongPass:
                    GameManager.Instance.currentUIManager.showTipsWindow("账号密码错误");
                    break;

            }

            return;
        }
        switch ((CMD)msg.cmd)
        {   
            //如果是登录包
            case CMD.RspLogin:
                //如果获得了服务器的响应
                GameManager.Instance.loginSystem.HandleServerData(msg);
                break;
        }

    }
}
