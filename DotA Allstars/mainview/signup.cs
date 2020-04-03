using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DotA_Allstars.mainview
{
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
            Random rnd = new Random();
            int ra = rnd.Next(1, 10);
            int rb = rnd.Next(1, 10);
            a = ra;
            b = rb;
            c1.Text = a.ToString();
            c2.Text = b.ToString();
            this.SuspendLayout();
            this.ResumeLayout(true);
        }
        public int a;
        public int b;
        private static readonly HttpClient client = new HttpClient();

        //Do Connect
        public class status
        {
            public int statusCode { get; set; }
            public string message { get; set; }
        }

        private void Signup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread th = new Thread(NewFormLog);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void NewFormLog(object obj)
        {
            Application.Run(new login());
        }

        private void LinkLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SignupBt_Click(object sender, EventArgs e)
        {
            if(usname.Text == "" || passwd.Text == "" ||repasswd.Text == "")
            {
                sttR.Text = "Chưa nhập đủ thông tin.";
            }else if(usname.TextLength < 6 || passwd.TextLength < 6)
            {
                sttR.Text = "Tên tài khoản hoặc mật khẩu quá ngắn.";
            }else if(passwd.Text != repasswd.Text)
            {
                sttR.Text = "Hai mật khẩu không giống nhau.";
            }else if(capcha.Text != (a + b).ToString())
            {
                sttR.Text = "Trình Toán của bạn hơi kém.";
            }
            else
            {
                sttR.Text = "";
                signupBt.Visible = false;
                this.Enabled = false;

                var values = new Dictionary<string, string>
                {
                   { "username", usname.Text },
                   { "password", repasswd.Text }
                };

                var content = new FormUrlEncodedContent(values);

                Task.Run(async () => {
                    var response = await client.PostAsync("http://user-man.mobavietnam.com/mobaz-login/signup", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<status[]>("[" + responseString + "]");
                    switch (data[0].statusCode)
                    {
                        case 400:
                            Invoke((MethodInvoker)delegate
                            {
                                sttR.Text = "Tên đăng nhập đã tồn tại.";
                                signupBt.Visible = true;
                                this.Enabled = true;
                            });
                            break;
                        case 500:
                            Invoke((MethodInvoker)delegate
                            {
                                sttR.Text = "Lỗi máy chủ.";
                                signupBt.Visible = true;
                                this.Enabled = true;
                            });
                            break;
                        case 200:
                            Invoke((MethodInvoker)delegate
                            {
                                if (MessageBox.Show("Đăng kí thành công!", "MobaZ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                                {
                                    this.Close();
                                }
                            });
                            break;
                    }
                });
            }
        }

        private void PTop_MouseDown(object sender, MouseEventArgs e)
        {
            login.drag = true;
            login.start_point = new Point(e.X, e.Y);
        }

        private void PTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (login.drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - login.start_point.X, p.Y - login.start_point.Y);
            }
        }

        private void PTop_MouseUp(object sender, MouseEventArgs e)
        {
            login.drag = false;
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
            Regex regex = new Regex("[^0-9a-zA-Z\b-]+");
            e.Handled = regex.IsMatch(e.KeyChar.ToString());
        }

        private void Capcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex regex = new Regex("[^0-9\b]+");
            e.Handled = regex.IsMatch(e.KeyChar.ToString());
        }
    }
}
