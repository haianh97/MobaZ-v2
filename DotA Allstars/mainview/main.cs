using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DotA_Allstars
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            //GetRooms();
            
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        public static string name;
        Dictionary<string, string> rooms = new Dictionary<string, string>();
        private const int cGrip = 16;
        private const int cCaption = 32;

        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if(pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if(pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public void GetRooms()
        {
            string html = string.Empty;
            string url = @"https://mobaz-lan.glitch.me/list-room";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            JToken token = JObject.Parse("{\"rooms\":" + html + "}");
            JArray items = (JArray)token["rooms"];
            int length = items.Count;
            for (int i = 0; i < length; i++)
            {
                string namer = (string)token.SelectToken("rooms[" + i + "].name");
                string idr = (string)token.SelectToken("rooms[" + i + "].networkID");
                rooms.Add(namer, idr);
            }
            foreach (string key in rooms.Keys)
            {
                listRooms.Items.Add(key);
            }
        }

        private void ListRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listRooms.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
                {
                    processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                    processInfo.Arguments = "join " + rooms[listRooms.Items[index].ToString()];
                    Process.Start(processInfo);
                }
                else
                {
                    processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                    processInfo.Arguments = "join " + rooms[listRooms.Items[index].ToString()];
                    Process.Start(processInfo);
                }
            }
        }

        private void ClBt_Click(object sender, EventArgs e)
        {
            //LeaveR();
            Environment.Exit(1);
        }

        private void MmmBt_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MxmBt_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
