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
        base.OnConnected();
        Debug.Log("Server Connected");
    }
    protected override void OnReciveMsg(NetMsg msg)
    {
        base.OnReciveMsg(msg);
        Debug.Log("Server Response:" + msg.text);
    }

    protected override void OnDisConnected()
    {
        base.OnDisConnected();
        Debug.Log("Server DisConnected");
    }
}
