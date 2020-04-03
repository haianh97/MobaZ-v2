using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DotA_Allstars.mainview
{
    public partial class notice : Form
    {

        XmlDocument paket = new XmlDocument();

        public notice()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            paket.Load("paket.xml");
            XmlNode ntc = paket.SelectSingleNode("settings/notice");
            if (dsag.Checked == true)
                ntc.Attributes[0].Value = "1";
            else
                ntc.Attributes[0].Value = "0";
            this.Close();
        }
    }
}
