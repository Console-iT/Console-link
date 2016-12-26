using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Console_link.ServiceRef;

namespace Console_link
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Namespace = "Console_link", CallbackContract = typeof(ICallback))]
    public interface ILinkService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Message UploadContent(Message composite);

        // TODO: 在此添加您的服务操作
        [OperationContract]
        void SubscribeEvent();

        [OperationContract]
        void UnsubscribeEvent();

        [OperationContract]
        void Publish(Message data);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“server.ContractType”直接使用其中定义的数据类型。
    [DataContract]
    public class Message
    {
        byte encodingMethod = 0;
        string title;
        string content;

        /// <summary>
        /// 正文的编码类型：
        /// 0-纯文本 1-MarkDown 2-LaTeX
        /// </summary>
        [DataMember]
        public byte EncodingMethod
        {
            get { return encodingMethod; }
            set { encodingMethod = value; }
        }

        /// <summary>
        /// 传输内容的标题
        /// </summary>
        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 传输内容的正文
        /// </summary>
        [DataMember]
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        public Message(string titl, byte encod, string con)
        {
            title = titl;
            encodingMethod = encod;
            content = con;
        }

        public static implicit operator Message(ServiceRef.Message v)
        {
            throw new NotImplementedException();
        }
    }

    [ServiceContract]
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void CallBackMethod(Message data);
    }
}
