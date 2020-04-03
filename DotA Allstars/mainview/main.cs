using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TechLifeForum;
using System.IO.Compression;
using System.Xml;
using System.Reflection;
using DotA_Allstars.mainview;
using System.Threading;

namespace DotA_Allstars
{

    public partial class main : Form
    {
        IrcClient client;
        public main()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SuspendLayout();
            this.ResumeLayout(true);
            this.ResizeBegin += (s, e) => { this.SuspendLayout(); };
            this.ResizeEnd += (s, e) => { this.ResumeLayout(true); };
            ver.Text = "ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            GetRooms();
            settingG();
        }
        public int port = 6667;
        string ip = "irc.pvpgn.mobavietnam.com";
        string ServerPass = "b3APQdYe6ePwhc8X";
        public static string name;
        Dictionary<string, string> rooms = new Dictionary<string, string>();
        private const int cGrip = 16;
        private const int cCaption = 32;
        public static string serverj;
        public static string idroom;
        public string crew;
        Color color = Color.White;
        OpenFileDialog opf = new OpenFileDialog();
        XmlDocument paket = new XmlDocument();
        public static String responseData = String.Empty;
        public static string[] dataRequest = responseData.Split(' ');
        public static bool lsSvExist = false;

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
            JToken token = JObject.Parse("{\"rooms\":" + Properties.Resources.list_room + "}");
            JArray items = (JArray)token["rooms"];
            int length = items.Count;
            for (int i = 0; i < length; i++)
            {
                string namer = (string)token.SelectToken("rooms[" + i + "].name");
                string idr = (string)token.SelectToken("rooms[" + i + "].networkID");
                rooms.Add(namer, idr);
            }
            foreach (var pair in rooms)
            {
                listRooms.Items.Add(pair.Key);
            }
            try
            {
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III", true))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("Battle.net Gateways");
                        if (o != null)
                        {
                            lsSvExist = true;
                            List<string> lsServer = new List<string>();
                            lsServer.AddRange((string[])key.GetValue("Battle.net Gateways"));
                            for (int i = 0; i < lsServer.Count; i++)
                            {
                                if (lsServer[i] == "pvpgn.mobavietnam.com")
                                    goto Endloop;
                            }
                            lsServer.AddRange(new string[] { "pvpgn.mobavietnam.com", "7", "mobavietnam.com" });
                            lsServer[1] = (((lsServer.Count - 2) / 3) - 1).ToString();
                            key.SetValue("Battle.net Gateways", lsServer.ToArray());
                            using (Microsoft.Win32.RegistryKey ubn = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\String", true))
                            {
                                if (ubn != null)
                                {
                                    Object u = ubn.GetValue("userbnet");
                                    if (u != null)
                                    {
                                        ubn.SetValue("userbnet", name.Trim());
                                    }
                                }
                            }
                        Endloop:
                            {
                                using (Microsoft.Win32.RegistryKey ubn = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\String", true))
                                {
                                    if (ubn != null)
                                    {
                                        Object u = ubn.GetValue("userbnet");
                                        if (u != null)
                                        {
                                            ubn.SetValue("userbnet", name.Trim());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }  
        }

        private void ListRooms_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listRooms.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listRooms.Enabled = false;
                idroom = rooms[listRooms.Items[index].ToString().Substring(0)];
                crew = "#" + idroom;
                serverj = "join " + rooms[listRooms.Items[index].ToString().Substring(0)];
                Success();
            }
        }

        private void ClBt_Click(object sender, EventArgs e)
        {
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
            bgrroom.Visible = false;
            DoConnect();
        }

        private void DoConnect()
        {
            client = new IrcClient(ip.Trim(), port, false, ServerPass);
            AddEvents();
            client.Nick = name.Trim();
            rtbOutput.Clear(); // in case they reconnect and have old stuff there
            rtbOutput.Text = Properties.Resources.wellcome;
            client.Connect();
            if(login.noticeO == "0")
            {
                notice nf = new notice();
                nf.ShowDialog();
            }
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

                    AddToChatWindow(name + ": " + txtSend.Text.Trim());
                    txtSend.Clear();
                    txtSend.Focus();
                }
            }
        }

        private void AddToChatWindow(string message)
        {
            rtbOutput.SelectionColor = color;
            rtbOutput.AppendText("[" + DateTimeOffset.Now.ToString("hh:mm:ss") + "] - " + message + "\n");
            rtbOutput.ScrollToCaret();
        }

        #region Event Listeners

        void client_OnConnect(object sender, EventArgs e)
        {
            rtbOutput.Enabled = true;
            rtbOutput.Visible = true;
            txtSend.Enabled = true;
            txtSend.Visible = true;
            /* btnColor.Enabled = true;
             btnColor.Visible = true;*/
            lstUsers.Enabled = true;
            lstUsers.Visible = true;
            searchPing.Visible = true;
            btnSetting.Enabled = true;
            btnSetting.Visible = true;
            btnStart.Enabled = true;
            btnStart.Visible = true;
            CnRoom();
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
                    if (gameVer.FileVersion != "1, 26, 0, 6401" || !File.Exists(Path.GetDirectoryName(pathwar3.Text) + "\\w3l.exe"))
                    {
                        pathwar3.Enabled = false;
                        btnBrower.Enabled = false;
                        btnStart.Enabled = false;
                        btnSave.Enabled = false;
                        using (WebClient wcp = new WebClient())
                        {
                            wcp.DownloadProgressChanged += wc_DownloadProgressChangedP;
                            wcp.DownloadFileAsync(
                                new Uri("http://103.56.157.165/TFTVersion1.26a.new.zip"),
                                Path.GetDirectoryName(pathwar3.Text) + "\\TFTVersion1.26a.new.zip"
                            );
                        }

                    }
                    try
                    {
                        if (!File.Exists(System.IO.Path.GetDirectoryName(pathwar3.Text) + "\\Maps\\DotA-6.83d-MobaZ-v1.0.w3x"))
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
            sttDl.Text = "1.26a new downloading..." + e.ProgressPercentage + "%";
            if (e.ProgressPercentage == 100)
            {
                sttDl.Text = "Extract...";
                doneDownloadP();
            }
        }

        public async void doneDownloadP()
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(Path.GetDirectoryName(pathwar3.Text) + "\\TFTVersion1.26a.new.zip"))
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
            catch
            {
                MessageBox.Show("Warcraft III đang chạy ngầm. Hãy tắt và thử lại");
            }
            
        }
        public void settingG()
        {
            pathwar3.Text = login.path;
            if (login.tagetW == "1")
                window.Checked = true;
            else
                window.Checked = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            paket.Load("paket.xml");
            XmlNode save = paket.SelectSingleNode("settings/war3");
            XmlNode taget = paket.SelectSingleNode("settings/taget");
            save.Attributes[0].Value = pathwar3.Text;
            if (window.Checked == true)
                taget.Attributes[0].Value = "1";
            else
                taget.Attributes[0].Value = "0";

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
                        new System.Uri("http://103.56.157.165/DotA-6.83d-MobaZ-v1.0.w3x"),
                        System.IO.Path.GetDirectoryName(pathwar3.Text) + "\\Maps\\DotA-6.83d-MobaZ-v1.0.w3x"
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
            try
            {
                if(lsSvExist != true)
                {
                    try
                    {
                        using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III", true))
                        {
                            if (key != null)
                            {
                                Object o = key.GetValue("Battle.net Gateways");
                                if (o != null)
                                {
                                    lsSvExist = true;
                                    List<string> lsServer = new List<string>();
                                    lsServer.AddRange((string[])key.GetValue("Battle.net Gateways"));
                                    for (int i = 0; i < lsServer.Count; i++)
                                    {
                                        if (lsServer[i] == "pvpgn.mobavietnam.com")
                                            goto Endloop;
                                    }
                                    lsServer.AddRange(new string[] { "pvpgn.mobavietnam.com", "7", "mobavietnam.com" });
                                    lsServer[1] = (((lsServer.Count - 2) / 3) - 1).ToString();
                                    key.SetValue("Battle.net Gateways", lsServer.ToArray());
                                    using (Microsoft.Win32.RegistryKey ubn = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\String", true))
                                    {
                                        if (ubn != null)
                                        {
                                            Object u = ubn.GetValue("userbnet");
                                            if (u != null)
                                            {
                                                ubn.SetValue("userbnet", name.Trim());
                                            }
                                        }
                                    }
                                Endloop:
                                    {
                                        using (Microsoft.Win32.RegistryKey ubn = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Blizzard Entertainment\\Warcraft III\\String", true))
                                        {
                                            if (ubn != null)
                                            {
                                                Object u = ubn.GetValue("userbnet");
                                                if (u != null)
                                                {
                                                    ubn.SetValue("userbnet", name.Trim());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                var gameVer = FileVersionInfo.GetVersionInfo(pathwar3.Text);
                if (gameVer.FileVersion != "1, 26, 0, 6401" || !File.Exists(Path.GetDirectoryName(pathwar3.Text) + "\\w3l.exe"))
                {
                    MessageBox.Show("Lỗi phiên bản, mời cập nhập lên 1.26a new", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process RunGame = new Process();
                    RunGame.StartInfo.UseShellExecute = false;
                    RunGame.StartInfo.WorkingDirectory = Path.GetDirectoryName(pathwar3.Text);
                    RunGame.StartInfo.FileName = Path.GetDirectoryName(pathwar3.Text) + "\\w3l.exe";
                    if (window.Checked == true)
                        RunGame.StartInfo.Arguments = string.Concat(" -window");
                    RunGame.Start();
                    
                    this.WindowState = FormWindowState.Minimized;
                }
            }
            catch
            {
                MessageBox.Show("Sai đường dẫn đến war3.exe hoặc trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LeaveN()
        {
            Invoke((MethodInvoker)delegate
            {
                bgrroom.Visible = true;
                rtbOutput.Enabled = false;
                rtbOutput.Visible = false;
                txtSend.Enabled = false;
                txtSend.Visible = false;
                /*btnColor.Enabled = false;
                btnColor.Visible = false;*/
                lstUsers.Enabled = false;
                lstUsers.Visible = false;
                searchPing.Visible = false;
                btnSetting.Enabled = false;
                btnSetting.Visible = false;
                btnStart.Enabled = false;
                btnStart.Visible = false;
                DcnRoom();
                DoDisconnect();
                listRooms.Enabled = true;
                rooms.Clear();
                listRooms.Items.Clear();
                GetRooms();
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("war3"))
            {
                if (process.ProcessName == "War3" || process.ProcessName == "war3")
                {
                    MessageBox.Show("Warcraft III đang chạy!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    goto DontRun;
                }
                else
                {
                    goto EndLoop;
                }
            }
        EndLoop:
            {
                if(btnHost.Enabled == false)
                {
                    MessageBox.Show("Bạn đã tạo một host, vui lòng hủy trước khi thoát room!", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    LeaveN();
                }
            }
        DontRun:;
        }
        
        private void rtbOutput_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
        }

        public void CnRoom()
        {
            Invoke((MethodInvoker)delegate
            {
                button1.Visible = true;
                label1.Visible = true;
                label4.Visible = true;
                serverList.Visible = true;
                label5.Visible = true;
                mapList.Visible = true;
                btnHost.Visible = true;
                btnCCHost.Visible = true;
                btnClr.Visible = true;
            });
        }
        public void DcnRoom()
        {
            button1.Visible = false;
            label1.Visible = false;
            label4.Visible = false;
            serverList.Visible = false;
            label5.Visible = false;
            mapList.Visible = false;
            btnHost.Visible = false;
            btnCCHost.Visible = false;
            btnClr.Visible = false;
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            string zone = string.Empty;
            string loadmap = string.Empty;
            switch (serverList.selectedIndex)
            {
                case 0:
                    zone = "!";
                    break;
                case 1:
                    zone = "@";
                    break;
                case 2:
                    zone = ">";
                    break;
                case 3:
                    zone = "$";
                    break;
                case 4:
                    zone = "%";
                    break;
                case 5:
                    zone = "^";
                    break;
                case 6:
                    zone = "&";
                    break;
                case 7:
                    zone = "*";
                    break;
            }

            switch (mapList.selectedIndex)
            {
                case 0:
                    loadmap = "load dota683dmobaz";
                    break;
                case 1:
                    loadmap = "load dota683d";
                    break;
                case 2:
                    loadmap = "load dota685k";
                    break;
                case 3:
                    loadmap = "load lod685i";
                    break;
                case 4:
                    loadmap = "load lod674c";
                    break;
                case 5:
                    loadmap = "load imba26en";
                    break;
                case 6:
                    loadmap = "load imba2018v4en";
                    break;
                case 7:
                    loadmap = "load legend99";
                    break;
                case 8:
                    loadmap = "load tonghop49";
                    break;
                case 9:
                    loadmap = "load kiemthien8";
                    break;
                case 10:
                    loadmap = "load legiontd41x20";
                    break;
                case 11:
                    loadmap = "load pokemonfinal";
                    break;
                case 12:
                    loadmap = "load warlock102";
                    break;
                case 13:
                    loadmap = "load greentd99";
                    break;
                case 14:
                    loadmap = "load divide120q";
                    break;
            }

            Invoke((MethodInvoker)delegate
            {
                AddToChatWindow(zone + loadmap);
                Thread.Sleep(2000);
                AddToChatWindow(zone + "pub " + name.Trim());
                btnHost.Enabled = false;
            });
        }

        private void btnCCHost_Click(object sender, EventArgs e)
        {
            string zone = string.Empty;
            switch (serverList.selectedIndex)
            {
                case 0:
                    zone = "!";
                    break;
                case 1:
                    zone = "@";
                    break;
                case 2:
                    zone = "#";
                    break;
                case 3:
                    zone = "$";
                    break;
                case 4:
                    zone = "%";
                    break;
                case 5:
                    zone = "^";
                    break;
                case 6:
                    zone = "&";
                    break;
                case 7:
                    zone = "*";
                    break;
                case 8:
                    zone = "(";
                    break;
                case 9:
                    zone = ")";
                    break;
            }
            AddToChatWindow(zone + "unhost " + name.Trim());
            Thread.Sleep(2000);
            btnHost.Enabled = true;
        }
    }
}