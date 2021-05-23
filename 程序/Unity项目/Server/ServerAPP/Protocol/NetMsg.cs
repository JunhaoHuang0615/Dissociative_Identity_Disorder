using System;
using PENet;

namespace Protocol
{   
    //网络传输内容以及配置类
    [Serializable]
    public class NetMsg : PEMsg
    {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
    }

    //玩家的数据信息,是需要传送的
    [Serializable]
    public class PlayerData
    {
        public int id;
        public string name;
        public int lv;
        public int exp;
        public int power;
        public int coin;
        public int diamond;

    }

    //接收到登录请求的信息
    [Serializable]
    public class ReqLogin
    {
        public string account;
        public string password;
    }

    //服务器登录回应的信息
    [Serializable]
    public class RspLogin
    {
        //回应的时候还需要回应玩家的数据
        public PlayerData playerData;
     //TODO : 是否可以登录
    }
    //这个是用来区分每次收到的消息是用来干嘛的,请求种类
    public enum CMD
    {
        None =0,
        //登录相关 100开始
        ReqLogin = 100,
        RspLogin = 102,

    }

    //这个是用来记录错的的中路
    public enum ErrCode
    {
        None = 0, //没有错误
        AcctIsOnline, //账号已经上线
        WrongPass,//密码码错误
    }
    public class IPConfig
    {

        public const string srvIP = "127.0.0.1";
        public const int srvport = 17666;
    }
}
