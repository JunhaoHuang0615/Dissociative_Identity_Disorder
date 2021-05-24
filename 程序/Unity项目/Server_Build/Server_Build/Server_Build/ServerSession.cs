using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PENet;
using Protocol;
//网络会话Session
public class ServerSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
        base.OnConnected();
        PETool.LogMsg("Client Connected");
    }
    protected override void OnReciveMsg(NetMsg msg)
    {
        base.OnReciveMsg(msg);
        PETool.LogMsg("Receieved CMD:" + msg.cmd);
        MsgPack msgPack = new MsgPack(msg, this);
        NetSvc.Instance.AddMsyQue(msgPack);
    }

    protected override void OnDisConnected()
    {
        base.OnDisConnected();
        PETool.LogMsg("Client DisConnected");
    }
}
