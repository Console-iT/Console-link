using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Console_link.ServiceRef;

namespace Console_link
{
    public class LinkClient : ILinkServiceCallback
    {
        private LinkServiceClient proxy;
        private Transfer mydata;

        public delegate void DataRenewHandler();
        public event DataRenewHandler DataRenewed;

        public LinkClient()
        {
            InstanceContext context = new InstanceContext(this);
            proxy = new LinkServiceClient(context);
            proxy.SubscribeEvent();
        }

        public void CallBackMethod(Transfer data)
        {
            mydata = data;
            DataRenewed();
        }

        public string GetTitle() { return mydata.Title; }
        public string GetContent() { return mydata.Content; }
        public byte GetEncoding() { return mydata.EncodingMethod; }

        public void Close()
        {
            proxy.UnsubscribeEvent();
            proxy.Close();
        }
    }
}
