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

        public LinkClient(string remoteAddress)
        {
            InstanceContext context = new InstanceContext(this);
            WSDualHttpBinding binding = new WSDualHttpBinding();
            Uri address = new Uri(remoteAddress);
            EndpointAddress endpointAddress = new EndpointAddress(address);
            //proxy = new LinkServiceClient(context);
            proxy = new LinkServiceClient(context, binding, endpointAddress);
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
