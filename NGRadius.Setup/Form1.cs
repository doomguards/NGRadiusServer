using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WinServiceSetup;

namespace NGRadius.Setup
{
    public partial class Form1 : Form
    {

        private WinServiceMgr Mgr = new WinServiceMgr("NGRadius.WinServer", "网络接入认证服务");
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {


            SetBtnEnabled(false);
            Action act = () =>
            {
                try
                {
                    Mgr.Install(true);
                    Mgr.ControlService(true);
                    Mgr.AddFireWall();

                    MessageBox.Show("服务安装成功", "提示", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK);
                }

            };
            var ar = act.BeginInvoke(null, null);
            while (!ar.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            SetBtnEnabled(true);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            SetBtnEnabled(false);
            Action act = () =>
            {
                try
                {
                    if (Mgr.IsInstalled == true)
                    {
                        Mgr.ControlService(false);
                    }
                    Mgr.Install(false);

                    MessageBox.Show("服务已经卸载", "提示", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK);
                }

            };
            var ar = act.BeginInvoke(null, null);
            while (!ar.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
            SetBtnEnabled(true);
        }
        private void SetBtnEnabled(bool v)
        {
            btnUninstall.Enabled = v;
            btnInstall.Enabled = v;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
