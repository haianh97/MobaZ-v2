using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Diagnostics;
using DotA_Allstars.mainview;
using System.Threading;
using System.Xml;
using System.IO;

namespace DotA_Allstars
{
    public partial class login : Form
    {
        public static bool drag = false;
        public static Point start_point = new Point(0, 0);
        public login()
        {
            InitializeComponent();
            string procName = Process.GetCurrentProcess().ProcessName;       
            Process[] processes = Process.GetProcessesByName(procName);
            

            if (processes.Length > 1)
            {
                if (MessageBox.Show(procName + " already running", "MobaZ", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Environment.Exit(1);
                }

            }
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
        OpenFileDialog opf = new OpenFileDialog();
        XmlDocument paket = new XmlDocument();

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
                this.Enabled = false;

                var values = new Dictionary<string, string>
                {
                   { "name", usname.Text },
                   { "password", paswd.Text }
                };

                var content = new FormUrlEncodedContent(values);
                Task.Run(async () => {
                    var response = await connect.PostAsync("https://mobaz-auth.glitch.me/login", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (responseString == "OK")
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            main.name = usname.Text;
                            paket.Load("paket.xml");
                            XmlNode rem = paket.SelectSingleNode("settings/remem");
                            XmlNode us = paket.SelectSingleNode("settings/us");
                            XmlNode pw = paket.SelectSingleNode("settings/pw");
                            XmlNode save = paket.SelectSingleNode("settings/war3");
                            XmlNode taget = paket.SelectSingleNode("settings/taget");
                           // pathWar3.Text = save.Attributes[0].Value;
                           // Taget.Text = taget.Attributes[0].Value;
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
                    }
                    else
                        Invoke((MethodInvoker)delegate
                        {
                            sttLg.Text = "Tên đăng nhập hoặc mật khẩu sai.";
                            loginBt.Visible = true;
                            this.Enabled = true;
                        });
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
    }
}
