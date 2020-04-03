using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using DotA_Allstars.mainview;
using System.Threading;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using AutoUpdaterDotNET;
using Newtonsoft.Json;

namespace DotA_Allstars
{
   
    public partial class login : Form
    {
        public static bool drag = false;
        public static Point start_point = new Point(0, 0);
       
        public login()
        {
            InitializeComponent();
            this.SuspendLayout();
            this.ResumeLayout(true);
            
            AutoUpdater.Mandatory = true;
            AutoUpdater.UpdateMode = Mode.Forced;
            AutoUpdater.Start("http://103.56.157.165/ud.xml");

            //Check running
            /*string procName = Process.GetCurrentProcess().ProcessName;       
            Process[] processes = Process.GetProcessesByName(procName);
            if (processes.Length > 1)
            {
                if (MessageBox.Show(procName + " already running", "MobaZ", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Environment.Exit(1);
                }

            }*/

            //Checking setting
            if (!File.Exists("paket.xml") || File.Exists("paket.xml") && new FileInfo("paket.xml").Length == 0 || File.Exists("paket.xml") && new FileInfo("paket.xml").Length == 3)
            {
                XmlWriter crtxml = XmlWriter.Create("paket.xml");
                crtxml.WriteStartElement("settings");
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("remem");
                crtxml.WriteAttributeString("value", "0");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("us");
                crtxml.WriteAttributeString("value", "");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("pw");
                crtxml.WriteAttributeString("value", "");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("war3");
                crtxml.WriteAttributeString("value", "");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("taget");
                crtxml.WriteAttributeString("value", "");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");

                crtxml.WriteStartElement("notice");
                crtxml.WriteAttributeString("value", "0");
                crtxml.WriteEndElement();
                crtxml.WriteString("\r\n");
                crtxml.WriteEndElement();
                crtxml.Close();
            }
            else
            {
                paket.Load("paket.xml");
                XmlNode rem = paket.SelectSingleNode("settings/remem");
                XmlNode us = paket.SelectSingleNode("settings/us");
                XmlNode pw = paket.SelectSingleNode("settings/pw");
                if (rem.Attributes[0].Value == "1")
                {
                    remember.Checked = true;
                    usname.Text = us.Attributes[0].Value;
                    paswd.Text = pw.Attributes[0].Value;
                }
                else
                {
                    remember.Checked = false;
                    usname.Text = username;
                    paswd.Text = password;
                }
            }
        }

        public static readonly HttpClient connect = new HttpClient();
        public static string username;
        public static string password;
        public static string path;
        public static string tagetW;
        public static string noticeO;
        XmlDocument paket = new XmlDocument();

        //Do Connect
        public class status
        {
            public int statusCode { get; set; }
            public string message { get; set; }
        }

        private void LoginBt_Click(object sender, EventArgs e)
        {
            if(usname.Text == "" || paswd.Text == "")
            {
                sttLg.Text = "Chưa nhập Tên đăng nhập hoặc mật khẩu";
            }
            else
            {
                //set disable
                sttLg.Text = "";
                loginBt.Visible = false;
                usname.Enabled = false;
                paswd.Enabled = false;
                remember.Enabled = false;
                reglink.Enabled = false;

                var values = new Dictionary<string, string>
                {
                   { "username", usname.Text },
                   { "password", paswd.Text }
                };

                var content = new FormUrlEncodedContent(values);
                
                Task.Run(async () => {
                    try
                    {
                        var response = await connect.PostAsync("http://user-man.mobavietnam.com/mobaz-login", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<status[]>("[" + responseString + "]");
                        switch (data[0].statusCode)
                        {
                            case 400:
                                Invoke((MethodInvoker)delegate
                                {
                                    sttLg.Text = "Tên đăng nhập hoặc mật khẩu sai.";
                                    loginBt.Visible = true;
                                    loginBt.Visible = true;
                                    usname.Enabled = true;
                                    paswd.Enabled = true;
                                    remember.Enabled = true;
                                    reglink.Enabled = true;
                                });
                                break;
                            case 500:
                                Invoke((MethodInvoker)delegate
                                {
                                    sttLg.Text = "Lỗi máy chủ.";
                                    loginBt.Visible = true;
                                    loginBt.Visible = true;
                                    usname.Enabled = true;
                                    paswd.Enabled = true;
                                    remember.Enabled = true;
                                    reglink.Enabled = true;
                                });
                                break;
                            case 200:
                                Invoke((MethodInvoker)delegate
                                {
                                    main.name = usname.Text;
                                    paket.Load("paket.xml");
                                    XmlNode rem = paket.SelectSingleNode("settings/remem");
                                    XmlNode us = paket.SelectSingleNode("settings/us");
                                    XmlNode pw = paket.SelectSingleNode("settings/pw");
                                    XmlNode save = paket.SelectSingleNode("settings/war3");
                                    XmlNode taget = paket.SelectSingleNode("settings/taget");
                                    XmlNode ntc = paket.SelectSingleNode("settings/notice");
                                    path = save.Attributes[0].Value;
                                    tagetW = taget.Attributes[0].Value;
                                    noticeO = ntc.Attributes[0].Value;
                                    if (remember.Checked == true)
                                    {
                                        rem.Attributes[0].Value = "1";
                                        us.Attributes[0].Value = usname.Text;
                                        pw.Attributes[0].Value = paswd.Text;
                                        paket.Save("paket.xml");
                                    }
                                    else
                                    {
                                        rem.Attributes[0].Value = "0";
                                        us.Attributes[0].Value = "";
                                        pw.Attributes[0].Value = "";
                                        paket.Save("paket.xml");
                                    }
                                    this.Close();
                                    Thread th = new Thread(NewFormMain);
                                    th.SetApartmentState(ApartmentState.STA);
                                    th.Start();
                                });
                                break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Kết nối với máy chủ bị gián đoạn");
                    }
                }); 
            }
        }

        private void Reglink_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(NewFormReg);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        private void NewFormMain(object obj)
        {
            Application.Run(new main());
        }
        private void NewFormReg(object obj)
        {
            Application.Run(new signup());
        }

        private void PTop_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void PTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void PTop_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void ClBt_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void MmmBt_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void Usname_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex("[^0-9a-zA-Z.^-^_-`\b-]+");
            e.Handled = regex.IsMatch(e.KeyChar.ToString());
        }
    }
}
