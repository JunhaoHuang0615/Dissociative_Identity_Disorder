
//客户端工具类

using PENet;
public class PECommon
{   
    public enum LogType
    {
        Log = 0,
        Warn = 1,
        Error = 2,
        Info = 3,
    }
    public static void Log(string msg, LogType logtype = LogType.Log)
    {
        LogLevel lv = (LogLevel)logtype;
        PETool.LogMsg(msg, lv);
    }


}
