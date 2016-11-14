using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Console_link;

namespace client_holder
{
    public enum EncodType { 纯文本, MarkDown, LaTeX }

    public partial class Form1 : Form
    {
        public LinkClient link;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            link = new LinkClient(textBox3.Text);
            link.DataRenewed += Link_DataRenewed;
            label1.Text = "Connected";
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void Link_DataRenewed()
        {
            textBox2.Text = link.GetTitle();
            EncodType et = (EncodType)link.GetEncoding();
            textBox4.Text = et.ToString();
            textBox1.Text = link.GetContent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            link.DataRenewed -= Link_DataRenewed;
            link.Close();
            link = null;
            GC.Collect();
            label1.Text = "Not connected";
            button1.Enabled = true;
            button2.Enabled = false;
        }
    }
}
