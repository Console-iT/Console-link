using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Diagnostics;

namespace Console_link
{
    public class LinkHost
    {
        ServiceHost host;

        public int Clients
        {
            get { return LinkService.clients.Count; }
        }

        public string HostAddress { get; private set; }

        public LinkHost(string url)
        {
            urlacl(url);
            HostAddress = url;
            host = new ServiceHost(typeof(LinkService));
            Type contract = typeof(ILinkService);
            WSDualHttpBinding binding = new WSDualHttpBinding();
            Uri address = new Uri(url);
            host.AddServiceEndpoint(contract, binding, address);
            host.Open();
        }

        public void Publish(string titl, byte encod, string con)
        {
            foreach (var c in LinkService.clients)
                c.CallBackMethod(new Message(titl, encod, con));
        }

        public void Close()
        {
            host.Close();
        }

        private void urlacl(string link)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//要执行的程序名称 
            p.StartInfo.Arguments = "/c C:\\Windows\\System32\\cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;//重定向输入  
            p.StartInfo.RedirectStandardOutput = false; //不重定向输出  
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口 
            p.StartInfo.Verb = "RunAs";
            p.Start();//启动程序 
                      //向CMD窗口发送输入信息： 
            p.StandardInput.WriteLine("netsh http add urlacl url={0} sddl=\"D:(A;;GX;;;LS)\"", link); //10秒后重启（C#中可不好做哦） 
            p.StandardInput.WriteLine("&exit");
            p.StandardInput.AutoFlush = true;
            //获取CMD窗口的输出信息： 
            //p.BeginOutputReadLine();
            //string sOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit(100);
            p.Close();
            //"netsh http delete urlacl url={0}"
            //Console.WriteLine(sOutput);
        }
    }
}
