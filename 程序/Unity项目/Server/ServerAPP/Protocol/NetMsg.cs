using System;
using PENet;

namespace Protocol
{   
    //网络传输内容以及配置类
    [Serializable]
    public class NetMsg : PEMsg
    {
        public string text;
    }
    public class IPConfig
    {

        public const string srvIP = "127.0.0.1";
        public const int srvport = 17666;
    }
}
