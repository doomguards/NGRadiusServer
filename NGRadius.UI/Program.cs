using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace NGRadius.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (s, e) => {
                File.WriteAllText(@"C:\" + DateTime.Now.ToString("yyMMddhhmmss") + ".txt", e.Exception.Message);
            };
            Application.ThreadExit += (s, e) =>
            {
                File.WriteAllText(@"C:\" + DateTime.Now.ToString("yyMMddhhmmss") + ".txt","xxx");
            };
            Application.Run(new Form1());
        }
    }
}
