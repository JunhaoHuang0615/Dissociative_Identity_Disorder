using System;
using Protocol;
using PENet;
namespace Server
{
    class ServerStart
    {
        static void Main(string[] args)
        {
            PESocket<ServerSession, NetMsg> server = new PESocket<ServerSession, NetMsg>();
            server.StartAsServer(IPConfig.srvIP, IPConfig.srvport);
            ServerRoot.Instance.Init();
            while (true)
            {

            }

        }
    }
}
