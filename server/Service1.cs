using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Console_link
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LinkService : ILinkService
    {
        private Action<Message> transferEvent = delegate { };
        public static List<ICallback> clients;

        public LinkService()
        {
            clients = new List<ICallback>();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public Message UploadContent(Message composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.EncodingMethod == 0)
            {
                composite.Title += "Suffix";
            }
            return composite;
        }

        public void SubscribeEvent()
        {
            ICallback c = OperationContext.Current.GetCallbackChannel<ICallback>();
            clients.Add(c);
            //transferEvent += clientCallBack.CallBackMethod;
        }

        public void UnsubscribeEvent()
        {
            ICallback c = OperationContext.Current.GetCallbackChannel<ICallback>();
            clients.Remove(c);
            //transferEvent -= clientCallBack.CallBackMethod;
        }

        public void Publish(Message data)
        {
            foreach (var c in clients)
                c.CallBackMethod(data);
        }
    }
}
