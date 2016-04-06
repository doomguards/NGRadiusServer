using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using NGRadius.Core;

namespace NGRadius.WinServer
{
    public partial class Service1 : ServiceBase
    {
        NGRadiusServer Server = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (Server == null)
            {
                Server = new NGRadiusServer();
            }
            Server.Start();
        }

        protected override void OnStop()
        {
            if (Server != null)
            {
                Server.Stop();
            }
        }
    }
}
