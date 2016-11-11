using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using Console_link;
using System.Net;

namespace server_holder
{
    public partial class Form1 : Form
    {
        ServiceHost host;

        //获取内网IP
        private void GetInternalIP()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
                comboBox1.Items.Add(ip.ToString());
        }

        public Form1()
        {
            InitializeComponent();
            GetInternalIP();
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            host = new ServiceHost(typeof(LinkService));
            Type contract = typeof(ILinkService);
            WSDualHttpBinding binding = new WSDualHttpBinding();
            //Uri address = new Uri(string.Format("http://{0}:{1}/console-link/{2}", new string[] { comboBox1.SelectedText, textBox1.Text, textBox2.Text }));
            Uri address = new Uri("http://localhost:8733/Design_Time_Addresses/server/Service1/");
            host.AddServiceEndpoint(contract, binding, address);
            host.Open();
            label4.Text = "Started";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            host.Close();
            label4.Text = "Not Started";
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var c in LinkService.clients)
                c.CallBackMethod(new Transfer(textBox4.Text, ((byte)comboBox2.SelectedIndex), textBox5.Text));
        }
    }
}
