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

                                    Process q = new Process
                                    {
                                        StartInfo =
                                    {
                                        FileName = "netsh.exe",
                                        Arguments = $"advfirewall firewall set rule group=\"Network Discovery\" new enable=No",
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
                #region trash
                /*var values = adt.net_adapters();
                if (networkrun == false)
                {
                    foreach (string n in values)
                    {
                        if (n == "ZeroTier One Virtual Port")
                        {
                            networkrun = true;
                            
                            break;
                        }
                        else
                        {
                            RunCli();
                        }
                    }
                }
                else
                {
                    foreach (ManagementObject adapter in searcher.Get())
                    {
                        string nicName = adapter["NetConnectionID"].ToString();
                        foreach (ManagementObject configuration in adapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                        {
                            if (configuration["IPConnectionMetric"].ToString() == "1")
                            {
                                goto ENDOFLOOPS;
                            }
                            else
                            {
                                Process p = new Process
                                {
                                    StartInfo =
                                    {
                                        FileName = "netsh.exe",
                                        Arguments = $"interface ipv4 set interface \"{nicName}\" metric=1",
                                        UseShellExecute = false,
                                        RedirectStandardOutput = true
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

                                Process q = new Process
                                {
                                    StartInfo =
                                    {
                                        FileName = "netsh.exe",
                                        Arguments = $"advfirewall firewall set rule group=\"Network Discovery\" new enable=No",
                                        UseShellExecute = false,
                                        RedirectStandardOutput = true
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
                            }
                        }

                        
                    }
                    
                }*/
                #endregion
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
