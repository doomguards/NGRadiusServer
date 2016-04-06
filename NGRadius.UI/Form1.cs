using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NGRadius.Core;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace NGRadius.UI
{
    public partial class Form1 : Form
    {

        NGRadiusServer Server = null;

        String RequestStr = @"01 01 00 70 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 01 05 33 33 34 02 12 1E E3 4E 1C C2 1E 1D 80 F7 7E 3F 8E D3 8D 9C 67 1E 0A 38 34 30 39 30 32 31 35 1F 0A 36 32 37 35 30 31 31 34 06 06 00 00 00 01 08 06 FF 01 02 03 2A 06 00 00 00 64 2B 06 00 00 03 E8 2C 07 69 64 33 33 34 50 12 70 03 18 CE B8 23 E5 B7 7B D7 B0 7D CF C3 5F 9E";
        String RequestStr2 = @"01 01 00 2B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 05 33 33 34 02 12 02 14 5B CC 18 90 7D 32 B9 2F E1 B7 5A 4E 20 72";
        String RequestStr3 = @"01 00 00 9E EB B2 E8 D9 1E 52 10 03 FB E1 52 39 27 58 93 F0 01 0E 33 34 38 30 62 33 34 32 61 30 61 33 02 12 56 BE 23 D1 61 13 7F E5 95 21 CB 44 B9 32 D4 49 04 06 C0 A8 08 05 20 07 68 65 6C 6C 6F 1E 1A 41 30 2D 36 33 2D 39 31 2D 38 42 2D 30 39 2D 35 30 3A 48 44 41 50 30 35 1F 13 33 34 2D 38 30 2D 42 33 2D 34 32 2D 41 30 2D 41 33 3D 06 00 00 00 13 4D 18 43 4F 4E 4E 45 43 54 20 31 31 4D 62 70 73 20 38 30 32 2E 31 31 62 50 12 B8 4B 87 5E 53 77 2C FA 90 16 E3 B5 5F 4E CA FD ";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TestTime();
            //Console.WriteLine("xxx");
            //UdpClient client = new UdpClient(AddressFamily.InterNetwork);
            //client.Client.Bind(new IPEndPoint(IPAddress.Any, 1812));
            //IPEndPoint r=new IPEndPoint(0,0);
            //while (true)
            //{
            //    client.Receive(ref r);
            //    Console.WriteLine("yyy");
            //}

        }
        void TestTime()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                var arr = RequestStr3.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var bytes = new List<byte>();
                foreach (var item in arr)
                {
                    bytes.Add(byte.Parse(item, NumberStyles.HexNumber));
                }
                var requestPaket = RadiusPaket.Parser(bytes.ToArray());
                var myResponsePaket = RadiusPaket.Build("1111122222", requestPaket.Authenticator, 2, requestPaket.Id);

            }
            sw.Stop();

            Console.WriteLine("做1000次解析耗时:" +  sw.ElapsedMilliseconds +"毫秒");
        }
        void Test()
        {
            var arr = RequestStr3.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var bytes = new List<byte>();
            foreach (var item in arr)
            {
                bytes.Add(byte.Parse(item, NumberStyles.HexNumber));
            }
            var requestPaket = RadiusPaket.Parser(bytes.ToArray());
            var myResponsePaket = RadiusPaket.Build("1111122222", requestPaket.Authenticator,3, requestPaket.Id);

            //var responseStr = @"02-00-00-2C-57-CE-42-DB-EB-9F-DA-5D-B3-E5-DB-D0-9E-75-92-BA-1B-06-00-98-96-7F-50-12-D1-B1-36-29-F5-7D-1C-65-CB-BC-DA-57-DE-49-E7-3C";
            var responseStr = @"02-00-00-2C-57-CE-42-DB-EB-9F-DA-5D-B3-E5-DB-D0-9E-75-92-BA-1B-06-00-98-96-7F-50-12-D1-B1-36-29-F5-7D-1C-65-CB-BC-DA-57-DE-49-E7-3C";
            var responseArr = responseStr.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var responseBytes = new List<byte>();
            foreach (var item in responseArr)
            {
                responseBytes.Add(byte.Parse(item, NumberStyles.HexNumber));
            }
            var responsePaket = RadiusPaket.Parser(responseBytes.ToArray());

            Console.WriteLine("My :" + BitConverter.ToString(myResponsePaket.Paket));
            Console.WriteLine("Sys:" + BitConverter.ToString(responsePaket.Paket));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ent => ent.AddressFamily == AddressFamily.InterNetwork);
            txtIP.Text = ip == null ? "" : ip.ToString();
            txtPort.Text = "1812";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Server == null)
            {
                Server= new NGRadiusServer(txtIP.Text.Trim(),int.Parse(txtPort.Text));
            }
            if (button2.Text == "启动")
            {
                
                Server.Start();
                button2.Text = "停止";
                this.Text = "工作中...";
            }
            else
            {
                Server.Stop();
                button2.Text = "启动";
                this.Text = "关闭";
            }
        }
    }
}
