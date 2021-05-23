using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PENet;
using Protocol;
using UnityEngine;

public class ClientSession : PENet.PESession<NetMsg>
{
    protected override void OnConnected()
    {
        Debug.Log("Server Connected");
        /*GameManager.Instance.currentUIManager.showTipsWindow("Conneced to Server");*/
    }
    protected override void OnReciveMsg(NetMsg msg)
    {
/*        GameManager.Instance.currentUIManager.changeTipsText("收到登录请求");
        GameManager.Instance.shouldShowWindow = true;*/
        Debug.Log("Server Response:" + ((CMD)msg.cmd).ToString());
        NetSvc.Instance.AddNetMsg(msg);
    }

    protected override void OnDisConnected()
    {
/*        GameManager.Instance.currentUIManager.showTipsWindow("与服务器断开连接");*/
        base.OnDisConnected();
        Debug.Log("Server DisConnected");
    }
}
