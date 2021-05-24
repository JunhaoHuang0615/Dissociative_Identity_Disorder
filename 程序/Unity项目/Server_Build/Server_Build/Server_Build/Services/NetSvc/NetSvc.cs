using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PENet;
using Protocol;
//网络服务类

//这是一个包含了游戏的信息和客户端地址的类
public class MsgPack
{
    public NetMsg netMsg;
    public ServerSession serverSession;
    public MsgPack(NetMsg netMsg, ServerSession serverSession)
    {
        this.netMsg = netMsg;
        this.serverSession = serverSession;
    }

}
public class NetSvc
{
    //单例
    private static NetSvc instance = null;
    public static NetSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetSvc();
            }
            return instance;
        }
    }

    private Queue<MsgPack> messagePackQueue = new Queue<MsgPack>();
    //需要一个锁，因为会有很多玩家同时发消息，队列要一个一个进
    public static readonly string obj = "lock";

    public void Init()
    {
        PESocket<ServerSession, NetMsg> server = new PESocket<ServerSession, NetMsg>();
        server.StartAsServer(IPConfig.srvIP, IPConfig.srvport);
        PETool.LogMsg("NetSvc init done"); //这个日志有时间

    }

    //将接收到的消息放到队列中,serversession也需要，否则不知道是谁发过来的
    public void AddMsyQue(MsgPack msgPack)
    {
        lock (obj)
        {
            //加入队列中
            messagePackQueue.Enqueue(msgPack);
        }

    }

    //数据处理
    public void Update()
    {
        if (messagePackQueue.Count > 0)
        {
            PECommon.Log("PackCount" + messagePackQueue.Count);
            lock (obj)
            {
                MsgPack netMsgPack = messagePackQueue.Dequeue();
                HandOutMsg(netMsgPack);
            }
        }
    }

    //业务分发，准备分发到各个系统内去处理收到的包
    private void HandOutMsg(MsgPack netMsg)
    {
        switch ((CMD)netMsg.netMsg.cmd)
        {
            case CMD.ReqLogin: //如果是登录系统的包
                LoginSys.Instance.ReqLogin(netMsg);

                break;
        }
    }


}

