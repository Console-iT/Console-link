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
using Microsoft.VisualBasic;

namespace server_holder
{
    public partial class Form1 : Form
    {
        //ServiceHost host;
        //string link;
        LinkHost host;

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
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string link = string.Format("http://{0}:{1}/console-link/{2}", new string[] { comboBox1.Text, textBox1.Text, textBox2.Text });
            host = new LinkHost(link);
            label4.Enabled = true;
            label4.Text = "Started 点此复制链接";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            host.Close();
            host = null;
            GC.Collect();
            label4.Text = "Not Started";
            label4.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            host.Publish(textBox4.Text, ((byte)comboBox2.SelectedIndex), textBox5.Text);
        }

        private void label4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Interaction.InputBox("复制以下链接到剪贴板，并发送给客户端控制者", "server holder", host.HostAddress);
        }
    }
}
