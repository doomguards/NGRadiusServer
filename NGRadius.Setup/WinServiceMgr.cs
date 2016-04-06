using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.IO;
using System.Diagnostics;

namespace WinServiceSetup
{
    public class WinServiceMgr
    {
        public WinServiceMgr(string serviceName, string serviceDescription)
        {
            this.AgentServiceName = serviceName;
            this.AgentDescription = serviceDescription;
        }
        /// <summary>
        /// 服务名
        /// </summary>
        public String AgentServiceName
        {
            get;
            private set;

        }

        /// <summary>
        /// 服务描述
        /// </summary>
        public String AgentDescription
        {
            get;
            private set;
        }

        /// <summary>
        /// 安装、卸载 服务
        /// </summary>
        /// <param name="isinstall">是否安装</param>
        public void Install(Boolean isinstall)
        {
            String filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AgentServiceName + ".exe");
            if (isinstall)
            {
                RunSC("create " + AgentServiceName + " BinPath= \"" + filename + " -s\" start= auto DisplayName= " + AgentServiceName);
                RunSC("description " + AgentServiceName + " " + AgentDescription);
            }
            else
                RunSC("Delete " + AgentServiceName);
        }

        /// <summary>
        /// 启动、停止 服务
        /// </summary>
        /// <param name="isstart"></param>
        public void ControlService(Boolean isstart)
        {
            if (isstart)
                RunCmd("net start " + AgentServiceName, false, true);
            else
                RunCmd("net stop " + AgentServiceName, false, true);
        }

        public void AddFireWall()
        {
            String filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AgentServiceName + ".exe");
            RunCmd(string.Format("netsh firewall add allowedprogram {0} {1} ENABLE", filename, AgentServiceName), false, true);
        }
        /// <summary>
        /// 执行一个命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="showWindow"></param>
        /// <param name="waitForExit"></param>
        private void RunCmd(String cmd, Boolean showWindow, Boolean waitForExit)
        {
            Process p = new Process();
            ProcessStartInfo si = new ProcessStartInfo();
            String path = Environment.SystemDirectory;
            path = Path.Combine(path, @"cmd.exe");
            si.FileName = path;
            if (!cmd.StartsWith(@"/")) cmd = @"/c " + cmd;
            si.Arguments = cmd;
            si.UseShellExecute = false;
            si.CreateNoWindow = !showWindow;
            si.RedirectStandardOutput = true;
            si.RedirectStandardError = true;
            p.StartInfo = si;

            p.Start();
            if (waitForExit)
            {
                p.WaitForExit();

                ////if (UTrace.Debug)
                ////{
                ////    String str = p.StandardOutput.ReadToEnd();
                ////    if (!String.IsNullOrEmpty(str)) Log.WriteLine(str.Trim(new Char[] { '\r', '\n', '\t' }).Trim());
                ////    str = p.StandardError.ReadToEnd();
                ////    if (!String.IsNullOrEmpty(str)) Log.WriteLine(str.Trim(new Char[] { '\r', '\n', '\t' }).Trim());
                ////}
            }
        }

        /// <summary>
        /// 执行SC命令
        /// </summary>
        /// <param name="cmd"></param>
        private void RunSC(String cmd)
        {
            String path = Environment.SystemDirectory;
            path = Path.Combine(path, @"sc.exe");
            if (!File.Exists(path)) path = "sc.exe";
            if (!File.Exists(path)) return;
            RunCmd(path + " " + cmd, false, true);
        }

        /// <summary>
        /// 是否已安装
        /// </summary>
        public Boolean? IsInstalled
        {
            get
            {
                try
                {
                    ServiceController control = new ServiceController(AgentServiceName);
                    if (control == null) return false;
                    try
                    {
                        //尝试访问一下才知道是否已安装
                        Boolean b = control.CanShutdown;
                        return true;
                    }
                    catch { return false; }
                }
                catch { return null; }
            }
        }

        /// <summary>
        /// 是否已启动
        /// </summary>
        public Boolean? IsRunning
        {
            get
            {
                try
                {
                    ServiceController control = new ServiceController(AgentServiceName);
                    if (control == null) return null;
                    if (control.Status == ServiceControllerStatus.Running) return true;
                    if (control.Status == ServiceControllerStatus.Stopped) return false;
                    return null;
                }
                catch { return null; }
            }
        }
    }
}
