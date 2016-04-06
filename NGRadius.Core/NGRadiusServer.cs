using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using F.Studio.Common.Cfg;
namespace NGRadius.Core
{
    public class NGRadiusServer :IDisposable
    {
        private UdpClient Server = null;
        private System.Timers.Timer CacheMonitor = null;
        public String IP { get; private set; }
        public int Port { get; private set; } //1812

        private bool IsRuning = false;
        private bool Enabled = false;
        private bool disposed = false;

        public NGRadiusServer()
        {
            try
            {
                var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ent => ent.AddressFamily == AddressFamily.InterNetwork);
                if (ip == null) throw new Exception("无法获取本机IP");
                this.IP = SimpleCfgMgr.GetV<String>("IP", ip.ToString());
                this.Port = SimpleCfgMgr.GetV<int>("Port", 1812);
            }
            catch (Exception ex)
            {
                LogErr(ex);
                throw;
            }
        }

        public NGRadiusServer(string ip, int port)
        {
            
            this.IP = ip;
            this.Port = port;
            
        }


        public void Start()
        {
            if (IsRuning) return;

            Enabled = true;
            IsRuning = true;

            ThreadPool.QueueUserWorkItem((o) => { _Start(); });
            
        }
        public void Stop()
        {
            Enabled = false;
            try{ CacheMonitor.Stop();}catch { }
            try { CacheMonitor.Dispose(); }catch { }
            
            try
            {
                Server.Close();
                Server = null;
            }
            catch { }
        }
        private void _Start()
        {
            LogInfo("进入工作线程:" + Thread.CurrentThread.ManagedThreadId);
            #region 缓存检测
            var interval= SimpleCfgMgr.GetV<int>("CacheMonitorInterval", 3);
            CacheMonitor = new System.Timers.Timer(interval * 1000);
            CacheMonitor.Elapsed += (s, e) => 
            {
                try
                {
                    CacheMonitor.Stop();
                    AccountMgr.Instance.CheckUser("hi");
                    LogUtil.Debug("缓存检测:" + DateTime.Now);
                }
                catch { }
                finally
                {
                    CacheMonitor.Start();
                }
            };
            CacheMonitor.Start();

            #endregion
            using (Server = new UdpClient(AddressFamily.InterNetwork))
            {
                Server.Client.Bind(new IPEndPoint(IPAddress.Any, Port));
                //Server.AllowNatTraversal(true);
                var remote = new IPEndPoint(0, 0);
                try
                {

                    while (Enabled)
                    {
                        var receiveData = Server.Receive(ref remote);
                        var ctx = new HandelContext() { ReceiveData = receiveData, Server = Server, RemotePoint = remote };
                        ctx.BTime=Environment.TickCount;
                        //采用异步处理
                        ThreadPool.QueueUserWorkItem(o =>
                        {
                            HandleRequest(ctx);
                        }, null);
                    }

                }
                catch (Exception ex)
                {
                    LogErr(ex);
                }
                finally
                {
                    try { Server.Close(); }catch { }

                    IsRuning = false;
  
                    if (Enabled)
                    {
                        IsRuning = true;
                        ThreadPool.QueueUserWorkItem((o) => { _Start(); });
                    }

                    LogInfo("退出工作线程:" + Thread.CurrentThread.ManagedThreadId);
                }


            }
           
        }

        private void HandleRequest(HandelContext ctx)
        {
            try
            {
                var requestPacket = RadiusPaket.Parser(ctx.ReceiveData);
                if (requestPacket.Code != 1)
                {
                    LogInfo("收到其它类型请求:" + RadiusPaket.ToHexStr(requestPacket.Paket));
                    return; //只处理接入请求

                }

                var userAttr = requestPacket.Attributes.FirstOrDefault(ent => ent.AttrType == 1);
                if (userAttr == null) throw new Exception("请求包中无用户名属性!");
                var username = Encoding.Default.GetString(userAttr.Value).Trim();

                var code = AccountMgr.Instance.CheckUser(username) ? (byte)2 : (byte)3;
                var sharedSecret=SimpleCfgMgr.GetV<String>("SharedSecret","1111122222");
                var responsePaket= RadiusPaket.Build(sharedSecret, requestPacket.Authenticator, code, requestPacket.Id);
                if (ctx.Server != null)
                {
                    ctx.Server.Send(responsePaket.Paket, responsePaket.Length,ctx.RemotePoint);
                }
                ctx.ETime = Environment.TickCount;
                LogUtil.Debug(string.Format("{0}[{1}]的请求,消耗时:{2}毫秒", code == 2 ? "接收" : "拒绝", username,(ctx.ETime.Value - ctx.BTime.Value).ToString()));
            }
            catch (Exception ex)
            {
                LogErr(ex);
            }

        }

        private void LogErr(Exception ex)
        {
            LogUtil.LogErr(new Sys_Log { Err = ex,Flag=IP,LogType="错误" });
        }

        private void LogInfo(string msg)
        {
            LogUtil.LogInfo(new Sys_Log { Msg =msg,Flag=IP,LogType="消息" });
        }


        #region IDisposable Members
 
     /// <summary>
     /// Performs application-defined tasks associated with freeing, 
     /// releasing, or resetting unmanaged resources.
     /// </summary>
     public void Dispose()
     {
       Dispose(true);
       GC.SuppressFinalize(this);
     }
 
     /// <summary>
     /// Releases unmanaged and - optionally - managed resources
     /// </summary>
     /// <param name="disposing"><c>true</c> to release both managed 
     /// and unmanaged resources; <c>false</c> 
     /// to release only unmanaged resources.
     /// </param>
     protected virtual void Dispose(bool disposing)
     {
       if (!this.disposed)
       {
         if (disposing)
         {
           try
           {
             Stop();
           }
           catch 
           {
             
           }
         }
 
         disposed = true;
       }
     }
 
     #endregion

    }

    public class HandelContext
    {
        public byte[] ReceiveData { get; set; }
        public UdpClient Server { get; set; }
        public IPEndPoint RemotePoint { get; set; }
        public long? BTime { get; set; }
        public long? ETime { get; set; }
    }
}
