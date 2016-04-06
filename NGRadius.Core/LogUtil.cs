using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using F.Studio.Common.Cfg;

namespace NGRadius.Core
{
    public class LogUtil
    {
        public static  void Debug(string msg)
        {
            if (!SimpleCfgMgr.GetV<bool>("DebugInfo", false)) return;
            Console.WriteLine(msg);
        }
         public static void LogInfo(Sys_Log ent)
        {
            if (!SimpleCfgMgr.GetV<bool>("LogInfo", false)) return;

            Console.WriteLine(ent.Msg);

            StackFrame sf = new StackFrame(1);
            var methodInfo = sf.GetMethod();
            if (methodInfo.Name == "LogInfo")
            {
                methodInfo = new StackFrame(2).GetMethod();
            }
            var fname = methodInfo.DeclaringType.FullName + "->" + methodInfo.Name;

            Action act = () =>
            {
                using (var ctx = DBCtx.GetCtx())
                {
                    try
                    {

                        var logEntry = new Sys_Log();
                        logEntry.LogType = ent.LogType;
                        logEntry.AddTime = DateTime.Now;
                        logEntry.CodeSoure = fname;
                        logEntry.Msg = ent.Msg;
                        logEntry.Flag = ent.Flag;
                        ctx.Sys_Log.AddObject(logEntry);
                        ctx.SaveChanges();
                    }
                    catch { }
                }
            };
            act.BeginInvoke(null, null);


        }
 


        public static void LogErr(Sys_Log ent)
        {
            if (!SimpleCfgMgr.GetV<bool>("LogErr", false)) return;

            Console.WriteLine(ent.Err.Message);

            StackFrame sf = new StackFrame(1);
            var methodInfo = sf.GetMethod();
            if (methodInfo.Name == "LogErr")
            {
                methodInfo = new StackFrame(2).GetMethod();
            }
            var fname = methodInfo.DeclaringType.FullName + "->" + methodInfo.Name;

            Action act = () =>
            {
                using (var ctx = DBCtx.GetCtx())
                {
                    try
                    {

                        var logEntry = new Sys_Log();
                        logEntry.LogType = "错误";
                        logEntry.AddTime = DateTime.Now;
                        logEntry.CodeSoure = fname;
                        logEntry.Msg = ent.Err.ToString();
                        logEntry.Flag = ent.Flag;
                        ctx.Sys_Log.AddObject(logEntry);
                        ctx.SaveChanges();
                    }
                    catch { }
                }
            };
            act.BeginInvoke(null, null);
        }
    

    }
}
