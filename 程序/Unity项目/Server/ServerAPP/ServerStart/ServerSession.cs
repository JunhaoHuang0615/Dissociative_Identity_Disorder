using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PENet;
using Protocol;

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
        PETool.LogMsg("Message Receieved:"+ msg.text);
        SendMsg(new NetMsg { 
            text = "SrvReq" + msg.text
        });
    }

    protected override void OnDisConnected()
    {
        base.OnDisConnected();
        PETool.LogMsg("Client DisConnected");
    }
}
