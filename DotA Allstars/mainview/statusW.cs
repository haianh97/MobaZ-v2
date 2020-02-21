using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading;
using System.ServiceProcess;
using System.Windows.Threading;
using System.Net;

namespace DotA_Allstars.mainview
{
    public partial class statusW : Form
    {
        main mn;
        private string authtoken;

        public statusW(main th)
        {
            InitializeComponent();
            mn = th;
            this.SuspendLayout();
            this.ResumeLayout(true);


        }

        private void StatusW_Load(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }
        public void RunCli()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (!File.Exists(@"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat"))
            {
                processInfo.FileName = @"C:\Program Files\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = main.serverj;
                Process.Start(processInfo);
            }
            else
            {
                processInfo.FileName = @"C:\Program Files (x86)\ZeroTier\One\zerotier-cli.bat";
                processInfo.Arguments = main.serverj;
                Process.Start(processInfo);
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
            RunCli();
            var obj = new Adapters();
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_NetworkAdapter WHERE Name=\"ZeroTier One Virtual Port\"");
            while (true)
            {
                var values = obj.net_adapters();
                foreach (string nadt in values)
                { 
                    if (nadt == "ZeroTier One Virtual Port")
                    {
                        connectstt.Invoke((MethodInvoker)delegate
                        {
                            connectstt.Text = "Set Metrix...";

                        });
                        break;
                    }
                }
                #region  trash
                foreach (ManagementObject adapter in searcher.Get())
                {
                    foreach (ManagementObject configuration in adapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                    {
                        try
                        {
                            string nicName = adapter["NetConnectionID"].ToString();

                            if (configuration["IPConnectionMetric"].ToString() == "1")
                            {
                                goto ENDOFLOOPS;
                            }
                            else
                            {
                                //set metric
                                Process p = new Process
                                {
                                    StartInfo =
                                    {
                                        FileName = "cmd.exe",
                                        Arguments = $"/c netsh interface ipv4 set interface \"{nicName}\" metric=1",
                                        WindowStyle = ProcessWindowStyle.Hidden,
                                        CreateNoWindow = true
                                    }
                                };

                                bool started = p.Start();
                                if (!started)
                                {
                                    if (SpinWait.SpinUntil(() => p.HasExited, TimeSpan.FromSeconds(20)))
                                    {
                                        Thread.Sleep(2000);
                                        started = true;
                                    }
                                    started = false;
                                }

                                //off upnp
                                Process q = new Process
                                {
                                    StartInfo =
                                    {
                                        FileName = "cmd.exe",
                                        Arguments = $"/c sc config \"upnphost\" start= disabled",
                                        WindowStyle = ProcessWindowStyle.Hidden,
                                        CreateNoWindow = true
                                    }
                                };

                                bool starteded = q.Start();
                                if (!starteded)
                                {
                                    if (SpinWait.SpinUntil(() => q.HasExited, TimeSpan.FromSeconds(20)))
                                    {
                                        Thread.Sleep(2000);
                                        starteded = true;
                                    }
                                    starteded = false;
                                }
                                //
                                ServiceController service = new ServiceController("SSDPSRV");
                                if ((service.Status.Equals(ServiceControllerStatus.Running)))
                                {
                                    try
                                    {
                                        TimeSpan timeout = TimeSpan.FromMilliseconds(2000);
                                        service.Stop();
                                        service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                                        Process r = new Process
                                        {
                                            StartInfo =
                                                {
                                                    FileName = "cmd.exe",
                                                    Arguments = $"/c sc config \"SSDPSRV\" start= disabled",
                                                    WindowStyle = ProcessWindowStyle.Hidden,
                                                    CreateNoWindow = true
                                                }
                                        };

                                        bool startedede = r.Start();
                                        if (!startedede)
                                        {
                                            if (SpinWait.SpinUntil(() => r.HasExited, TimeSpan.FromSeconds(20)))
                                            {
                                                Thread.Sleep(2000);
                                                startedede = true;
                                            }
                                            startedede = false;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    Process r = new Process
                                    {
                                        StartInfo =
                                            {
                                                FileName = "cmd.exe",
                                                Arguments = $"/c sc config \"SSDPSRV\" start= disabled",
                                                WindowStyle = ProcessWindowStyle.Hidden,
                                                CreateNoWindow = true
                                            }
                                    };

                                    bool startedede = r.Start();
                                    if (!startedede)
                                    {
                                        if (SpinWait.SpinUntil(() => r.HasExited, TimeSpan.FromSeconds(20)))
                                        {
                                            Thread.Sleep(2000);
                                            startedede = true;
                                        }
                                        startedede = false;
                                    }
                                }

                            }
                        }

                        catch
                        {

                        }
                    }
                }
                #endregion
            }
        ENDOFLOOPS:
            {
                try
                {
                    Invoke(new MethodInvoker(delegate {
                        //mn.Success();
                        mn.settingG();
                        this.Close();
                    }));
                }
                catch
                {

                }
            }
        }

        private void ClBt_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Khi bạn tắt tiến trình này, bạn cần set metric bằng tay.") == DialogResult.OK)
            {
                try
                {
                    mn.Success();
                    mn.settingG();
                    mn.roomPlot();
                    this.Close();
                }
                catch
                {

                } 
            }
        }

        public void JoinNetwork(Dispatcher d, string nwid, bool allowManaged = true, bool allowGlobal = true, bool allowDefault = true)
        {
            Task.Factory.StartNew(() =>
            {
                var request = WebRequest.Create("127.0.0.1:9993/network/" + nwid + "?auth=" + authtoken) as HttpWebRequest;
                if (request == null)
                {
                    return;
                }

                request.Method = "POST";
                request.ContentType = "applicaiton/json";
                request.Timeout = 30000;
                try
                {
                    using (var streamWriter = new StreamWriter(((HttpWebRequest)request).GetRequestStream()))
                    {
                        string json = "{\"allowManaged\":" + (allowManaged ? "true" : "false") + "," +
                                "\"allowGlobal\":" + (allowGlobal ? "true" : "false") + "," +
                                "\"allowDefault\":" + (allowDefault ? "true" : "false") + "}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                catch (WebException)
                {
                    d.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        MessageBox.Show("Error Joining Network: Cannot connect to ZeroTier service.");
                    }));
                    return;
                }

                try
                {
                    var httpResponse = (HttpWebResponse)request.GetResponse();

                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        
                    }
                    else if (httpResponse.StatusCode != HttpStatusCode.OK)
                    {
                        Console.WriteLine("Error sending join network message");
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    d.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        MessageBox.Show("Error Joining Network: Cannot connect to ZeroTier service.");
                    }));
                }
                catch (System.Net.WebException e)
                {
                    HttpWebResponse res = (HttpWebResponse)e.Response;
                    if (res != null && res.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        
                    }
                    d.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                    {
                        MessageBox.Show("Error Joining Network: Cannot connect to ZeroTier service.");
                    }));
                }
            });
        }

    }
}
