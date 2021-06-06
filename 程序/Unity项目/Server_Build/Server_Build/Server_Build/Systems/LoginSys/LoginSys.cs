using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;


public class LoginSys
{
    //业务系统都是单例
    private static LoginSys instance = null;
    public static LoginSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginSys();
            }
            return instance;
        }
    }

    public void Init()
    {
        PECommon.Log("LoginSvc Init done");
    }

    public void ReqLogin(MsgPack netMsgPack)
    {
        ReqLogin data = netMsgPack.netMsg.reqLogin;
        //回应客户端的包
        NetMsg rspMsg = new NetMsg
        {
            cmd = (int)CMD.RspLogin,
            rspLogin = new RspLogin(),
        };
        //判断当前账号是否已经上线
        //已上线：返回错误信息
        if (CacheSvc.Instance.IsAcctOnLine(data.account))
        {
            rspMsg.err = (int)ErrCode.AcctIsOnline;
        }
        else
        {
            //未上线：
            //账号是否存在
            PlayerData playerData = CacheSvc.Instance.GetPlayerData(data.account, data.password);
            if (playerData == null)
            {
                //存在：检测密码错误
                rspMsg.err = (int)ErrCode.WrongPass;
            }
            else
            {
                rspMsg.rspLogin = new RspLogin
                {
                    playerData = playerData
                };
                CacheSvc.Instance.AcctOnLine(data.account, netMsgPack.serverSession, playerData);
            }

            //账号不存在
            //创建默认的账号密码
        }
        netMsgPack.serverSession.SendMsg(rspMsg);
    }

    //收到命名请求之后：
    public void ReqRename(MsgPack netMsgPack)
    {
        ReqRename data = netMsgPack.netMsg.reqRename;
        NetMsg msg = new NetMsg
        {
            cmd = (int)CMD.RspRename
        };

        if (CacheSvc.Instance.IsNameExist(data.name))
        {
            //名字是否已经存在
            //存在：返回错误码
            msg.err = (int)ErrCode.NameIsExist;
        }
        else
        {
            //不存在：更新缓存，以及数据库，再返回给客户端
            //还有一种做法是更新缓存之后，马上返回客户端，但是没办法知道数据库是否更新成功
            //如果提前返回客户端，必须要设定机制数据库更新是成功的
            PlayerData playerData = CacheSvc.Instance.GetPlayerDataBySession(netMsgPack.serverSession);
            playerData.name = data.name; //将缓存的数据更新了
            if (!CacheSvc.Instance.UpdatePlayerData(playerData.id, playerData))
            {
                msg.err = (int)ErrCode.UpdateDBError;
            }
            else
            {
                msg.rspRename = new RspRename
                {
                    name = data.name
                };
            }
        }
        netMsgPack.serverSession.SendMsg(msg);
    }
}


