/***
 *
 * Title:"" 项目：AAA
 * 主题：
 * Description:
 * 功能：
 *
 * Date:2021/
 * Version:0.1v
 * Coder:Junhao Huang
 * email:huangjunhao0615@gmail.com
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

public class ClientStart : MonoBehaviour
{
    PENet.PESocket<ClientSession, NetMsg> client = null;
    private void Start()
    {
        client = new PENet.PESocket<ClientSession, NetMsg>();
        client.StartAsClient(IPConfig.srvIP, IPConfig.srvport);
        client.SetLog(true, (string msg, int lv) => {
        
            switch (lv)
            {
                case 0: //当日志等级是0的时候
                    msg = "Log" + msg;
                    Debug.Log(msg);
                    break;
                case 1: //当日志等级是0的时候
                    msg = "Warn" + msg;
                    Debug.Log(msg);
                    break;
                case 2: //当日志等级是0的时候
                    msg = "Error" + msg;
                    Debug.Log(msg);
                    break;
                case 3: //当日志等级是0的时候
                    msg = "Info" + msg;
                    Debug.Log(msg);
                    break;

            }
        
        });

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            client.session.SendMsg(new NetMsg
            {
                text = "this is from client"
            }
                );
        }
    }
}
