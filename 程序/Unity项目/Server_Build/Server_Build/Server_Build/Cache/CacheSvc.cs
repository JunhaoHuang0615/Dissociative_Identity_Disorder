using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;

//服务器缓存层
//将所有登录了的账号可以记录在这里，检查玩家是否已经上线就可以在这里检查
class CacheSvc
{
    //业务系统都是单例
    private static CacheSvc instance = null;
    public static CacheSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CacheSvc();
            }
            return instance;
        }
    }

    //数据库的管理需要被使用
    private DBMgr dBMgr;



    public void Init()
    {
        dBMgr = DBMgr.Instance;
        PECommon.Log("CacheSvc Init Done");
    }

    private Dictionary<string, ServerSession> onLineDict = new Dictionary<string, ServerSession>();
    //把上线了的玩家的数据直接缓存起来，就不需要再去数据库了   
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();
    //判断当前账号是否已经上线
    public bool IsAcctOnLine(string acct)
    {
        return onLineDict.ContainsKey(acct);
    }



    public PlayerData GetPlayerData(string acct, string pass)
    {
        //数据库链接，查找账号信息
        return dBMgr.QuaryPlayerData(acct, pass);
    }

    public bool IsNameExist(string name)
    {
        return DBMgr.Instance.QueryNameData(name);
    }

    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onLineSessionDic.TryGetValue(session, out PlayerData playerData))
        {
            return playerData;
        }
        else
        {
            return null;
        }
    }
    public bool UpdatePlayerData(int id, PlayerData playerData)
    {
        return DBMgr.Instance.UpdatePlayerData(id, playerData);
    }



    //新上线账号加入缓存
    public void AcctOnLine(string acct, ServerSession serverSession, PlayerData playerData)
    {
        onLineDict.Add(acct, serverSession);
        onLineSessionDic.Add(serverSession, playerData);

        //TODO： 玩家同一个ip在下线时要清空，否则重复登录会报错
    }
}

