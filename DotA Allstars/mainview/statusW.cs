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

namespace DotA_Allstars.mainview
{
    public partial class statusW : Form
    {
        main mn;
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
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_NetworkAdapter WHERE Name=\"ZeroTier One Virtual Port\"");
            //Adapter adt = new Adapter();
            
            while (true)
            {
                foreach (ManagementObject adapter in searcher.Get())
                {
                    if(adapter["Name"].ToString() == "ZeroTier One Virtual Port")
                    {
                        connectstt.Invoke((MethodInvoker)delegate
                        {
                            connectstt.Text = "Set Metrix, Off UPnP...";
                        });
                        string nicName = adapter["NetConnectionID"].ToString();
                        foreach (ManagementObject configuration in adapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                        {
                            try
                            {
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
                                        FileName = "netsh.exe",
                                        Arguments = $"interface ipv4 set interface \"{nicName}\" metric=1",
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
                    else
                    {
                        RunCli();
                    }
                }
            }
        ENDOFLOOPS:
            {
                Invoke(new MethodInvoker(delegate {
                    mn.Success();
                    mn.settingG();
                    this.Close();
                }));
            }
        }
    }
}
