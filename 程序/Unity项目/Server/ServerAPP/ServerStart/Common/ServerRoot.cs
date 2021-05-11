using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//功能： 服务器初始化

    class ServerRoot
    {

    private static ServerRoot instance = null;
    public static ServerRoot Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ServerRoot();
            }
            return instance;
        }
    }

    public void Init()
    {
        //数据库初始化

        //服务层初始化
        NetSvc.Instance.Init();

        //业务系统
        LoginSys.Instance.Init();

    }
}
