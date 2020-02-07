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
using DotA_Allstars.mainview;
using TechLifeForum;
using System.IO.Compression;
using System.Xml;
using System.Reflection;

namespace DotA_Allstars
{
    public partial class main : Form
    {
        IrcClient client;
        public main()
        {
            InitializeComponent();
            GetRooms();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SuspendLayout();
            this.ResumeLayout(true);
            this.ResizeBegin += (s, e) => { this.SuspendLayout(); };
            this.ResizeEnd += (s, e) => { this.ResumeLayout(true); };
            ver.Text = "ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        }
        public int port = 6667;
        string ip = "103.90.224.213";
        public static string name;
        Dictionary<string, string> rooms = new Dictionary<string, string>();
        private const int cGrip = 16;
        private const int cCaption = 32;
        public static string serverj;
        public static string idroom;
        public string crew = "#" + idroom;
        Color color = Color.White;
        OpenFileDialog opf = new OpenFileDialog();
        XmlDocument paket = new XmlDocument();

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
                roomP.Enabled = false;
                idroom = rooms[listRooms.Items[index].ToString()];
                serverj = "join " + rooms[listRooms.Items[index].ToString()];
                new statusW(this).ShowDialog();
            }
        }

        private void ClBt_Click(object sender, EventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
            {
                processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);

            }
            else
            {
                processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
            }
            
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

        public void Success()
        {
            rtbOutput.Enabled = true;
            rtbOutput.Visible = true;
            txtSend.Enabled = true;
            txtSend.Visible = true;
            btnColor.Enabled = true;
            btnColor.Visible = true;
            lstUsers.Enabled = true;
            lstUsers.Visible = true;
            searchPing.Visible = true;
            btnSetting.Enabled = true;
            btnSetting.Visible = true;
            btnStart.Enabled = true;
            btnStart.Visible = true;
            button1.Visible = true;
            DoConnect();
        }

        private void DoConnect()
        {

            client = new IrcClient(ip.Trim(), port, false);
            AddEvents();
            client.Nick = name.Trim();
            rtbOutput.Clear(); // in case they reconnect and have old stuff there
            client.Connect();
        }
        private void DoDisconnect()
        {
            lstUsers.Items.Clear();
            txtSend.Enabled = false;
            client.Disconnect();
            client = null;
        }

        private void AddEvents()
        {
            client.ChannelMessage += client_ChannelMessage;
            client.ExceptionThrown += client_ExceptionThrown;
            client.NoticeMessage += client_NoticeMessage;
            client.OnConnect += client_OnConnect;
            client.PrivateMessage += client_PrivateMessage;
            client.ServerMessage += client_ServerMessage;
            client.UpdateUsers += client_UpdateUsers;
            client.UserJoined += client_UserJoined;
            client.UserLeft += client_UserLeft;
            client.UserNickChange += client_UserNickChange;
        }

        private void TxtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (client.Connected && !String.IsNullOrEmpty(txtSend.Text.Trim()))
                {
                    if (crew.StartsWith("#"))
                        client.SendMessage(crew.Trim(), txtSend.Text.Trim());
                    else
                        client.SendMessage("#" + crew.Trim(), txtSend.Text.Trim());

                    AddToChatWindow("["+DateTime.Now.ToString("HH:MM")+"] - " + name + ": " + txtSend.Text.Trim());
                    txtSend.Clear();
                    txtSend.Focus();
                }
            }
        }

        private void AddToChatWindow(string message)
        {
            rtbOutput.SelectionColor = color;
            rtbOutput.AppendText(message + "\n");
            rtbOutput.ScrollToCaret();
        }

        #region Event Listeners

        void client_OnConnect(object sender, EventArgs e)
        {
            txtSend.Enabled = true;

            if (crew.StartsWith("#"))
                client.JoinChannel(crew.Trim());
            else
                client.JoinChannel("#" + crew.Trim());

        }

        void client_UserNickChange(object sender, UserNickChangedEventArgs e)
        {
            lstUsers.Items[lstUsers.Items.IndexOf(e.Old)] = e.New;
        }

        void client_UserLeft(object sender, UserLeftEventArgs e)
        {
            lstUsers.Items.Remove(e.User);
        }

        void client_UserJoined(object sender, UserJoinedEventArgs e)
        {
            lstUsers.Items.Add(e.User);
        }

        void client_UpdateUsers(object sender, UpdateUsersEventArgs e)
        {
            lstUsers.Items.Clear();
            lstUsers.Items.AddRange(e.UserList);

        }

        void client_ServerMessage(object sender, StringEventArgs e)
        {
            Console.WriteLine(e.Result);
        }

        void client_PrivateMessage(object sender, PrivateMessageEventArgs e)
        {
            AddToChatWindow("PM FROM " + e.From + ": " + e.Message);
        }

        void client_NoticeMessage(object sender, NoticeMessageEventArgs e)
        {
            AddToChatWindow("NOTICE FROM " + e.From + ": " + e.Message);
        }

        void client_ExceptionThrown(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        void client_ChannelMessage(object sender, ChannelMessageEventArgs e)
        {
            AddToChatWindow(e.From + ": " + e.Message);
        }
        #endregion

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if(settingP.Visible == true)
            {
                settingP.Visible = false;
                settingP.Enabled = false;
            }
            else
            {
                settingP.Visible = true;
                settingP.Enabled = true;
            }
            try
            {
                if (!File.Exists(System.IO.Path.GetDirectoryName(pathwar3.Text) + "\\Maps\\DotA-6.83d-MobaZ.w3x"))
                {
                    mapName.ForeColor = Color.Red;
                }
                else
                {
                    mapName.ForeColor = Color.Green;
                }
            }
            catch (ArgumentException)
            {
                mapName.ForeColor = Color.Red;
            }
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if(colorDialog1.Color == Color.Black)
                {
                    color = Color.White;
                }
                else
                {
                    color = colorDialog1.Color;
                }
            }
        }

        private void BtnBrower_Click(object sender, EventArgs e)
        {
            opf.Filter = "War3.exe |war3.exe";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(opf.FileName))
                {
                    pathwar3.Text = Path.Combine(Path.GetDirectoryName(opf.FileName), opf.FileName);
                    var gameVer = FileVersionInfo.GetVersionInfo(pathwar3.Text);
                    if (gameVer.FileVersion != "1, 26, 0, 6401")
                    {
                        pathwar3.Enabled = false;
                        btnBrower.Enabled = false;
                        btnStart.Enabled = false;
                        btnSave.Enabled = false;
                        using (WebClient wcp = new WebClient())
                        {
                            wcp.DownloadProgressChanged += wc_DownloadProgressChangedP;
                            wcp.DownloadFileAsync(
                                new Uri("http://103.90.225.234/NDPatchUpdate/TFTVersion1.26a.zip"),
                                Path.GetDirectoryName(pathwar3.Text) + "\\TFTVersion1.26a.zip"
                            );
                        }
                        
                    }
                    try
                    {
                        if (!File.Exists(System.IO.Path.GetDirectoryName(pathwar3.Text) + "\\Maps\\DotA-6.83d-MobaZ.w3x"))
                        {
                            mapName.ForeColor = Color.Red;
                        }
                        else
                        {
                            mapName.ForeColor = Color.Green;
                        }
                    }
                    catch (ArgumentException)
                    {
                        mapName.ForeColor = Color.Red;
                    }
                }
            }
        }

        void wc_DownloadProgressChangedP(object sender, DownloadProgressChangedEventArgs e)
        {
            sttDl.Text = "1.26a downloading..." + e.ProgressPercentage + "%";
            if (e.ProgressPercentage == 100)
            {
                sttDl.Text = "Extract...";
                doneDownloadP();
            }
        }

        public async void doneDownloadP()
        {
            //System.IO.Compression..ExtractToDirectory(System.IO.Path.GetDirectoryName(pathWar3.Text) + "\\TFTVersion1.26a.zip", System.IO.Path.GetDirectoryName(pathWar3.Text));
            using (ZipArchive archive = ZipFile.OpenRead(Path.GetDirectoryName(pathwar3.Text) + "\\TFTVersion1.26a.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    {
                        entry.ExtractToFile(Path.Combine(Path.GetDirectoryName(pathwar3.Text), entry.FullName), true);
                    }
                }
            }
            sttDl.Text = "Done!";
            await Task.Delay(2000);
            sttDl.Text = "";
            pathwar3.Enabled = true;
            btnBrower.Enabled = true;
            btnStart.Enabled = true;
            btnSave.Enabled = true;
        }
        public void settingG()
        {
            pathwar3.Text = login.path;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            paket.Load("paket.xml");
            XmlNode save = paket.SelectSingleNode("settings/war3");
            XmlNode taget = paket.SelectSingleNode("settings/taget");
            save.Attributes[0].Value = pathwar3.Text;
            taget.Attributes[0].Value = login.tagetW;
            paket.Save("paket.xml");
            sttSV.Visible = true;
            TheEnclosingMethod();
        }

        public async void TheEnclosingMethod()
        {
            await Task.Delay(3000);
            sttSV.Text = "";
            settingP.Visible = false;
        }

        private void MapName_Click(object sender, EventArgs e)
        {
            if (File.Exists(pathwar3.Text))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(
                        new System.Uri("http://103.90.225.234/NDPatchUpdate/DotA-6.83d-MobaZ.w3x"),
                        System.IO.Path.GetDirectoryName(pathwar3.Text) + "\\Maps\\DotA-6.83d-MobaZ.w3x"
                    );
                }
            }
            else
            {
                btnBrower.PerformClick();
            }
        }
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            sttDl.Text = "Map downloading..." + e.ProgressPercentage + "%";
            if (e.ProgressPercentage == 100)
            {
                sttDl.Text = "Done!";
                doneDownload();
            }
        }

        public async void doneDownload()
        {
            await Task.Delay(2000);
            sttDl.Text = "";
            mapName.ForeColor = Color.Green;
            mapName.Enabled = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("war3"))
            {
                if(process.ProcessName == "War3" || process.ProcessName == "war3")
                {
                    var ms = MessageBox.Show("Warcraft III đang chạy, nếu tiếp tục sẽ bị tắt và chạy lại. Bạn có muốn tiếp tục.", "Cảnh báo!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if(ms == DialogResult.OK)
                    {
                        process.Kill();
                    }
                    else
                    {
                        goto EndLoop;
                    }
                }
                
            }
            try
            {
                var gameVer = FileVersionInfo.GetVersionInfo(pathwar3.Text);
                if (gameVer.FileVersion != "1, 26, 0, 6401")
                {
                    MessageBox.Show("Lỗi phiên bản, mời cập nhập lên 1.26a", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process RunGame = new Process();
                    RunGame.StartInfo.FileName = pathwar3.Text;
                    RunGame.StartInfo.Arguments = string.Concat(" " + login.tagetW);
                    RunGame.Start();
                    //NameC();
                    this.WindowState = FormWindowState.Minimized;
                }

            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Sai đường dẫn đến war3.exe hoặc trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        EndLoop:;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
            {
                processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
                Close();
            }
            else
            {
                processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
                Close();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
            {
                processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
                
            }
            else
            {
                processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
                
            }
        }

        public void LeaveN()
        {
            rtbOutput.Enabled = false;
            rtbOutput.Visible = false;
            txtSend.Enabled = false;
            txtSend.Visible = false;
            btnColor.Enabled = false;
            btnColor.Visible = false;
            lstUsers.Enabled = false;
            lstUsers.Visible = false;
            searchPing.Visible = false;
            btnSetting.Enabled = false;
            btnSetting.Visible = false;
            btnStart.Enabled = false;
            btnStart.Visible = false;
            button1.Visible = false;
            DoDisconnect();
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
            {
                processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
                
            }
            else
            {
                processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = "leave " + idroom;
                Process.Start(processInfo);
            }
            roomP.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LeaveN();
        }
    }
}
