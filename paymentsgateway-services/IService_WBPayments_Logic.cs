using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WBPayments_Logic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService_WBPayments_Logic
    {
        [OperationContract]
        ReturnObject<int> CreatePayment(CreatePaymentObject createPaymentObject);

        [OperationContract]
        ReturnObject<int> ConfirmPayment(ConfirmPaymentObject confirmPaymentObject);

        [OperationContract]
        ReturnObject<bool> ValidateCredentials(String user, String pass);
    }

    [DataContract]
    public class CreatePaymentObject
    {
        [DataMember]
        public string ClientId { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string GatewayId { get; set; }

        [DataMember]
        public string CurrencyId { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }

    [DataContract]
    public class ConfirmPaymentObject
    {
        [DataMember]
        public string ClientId { get; set; }

        [DataMember]
        public int PaymentId { get; set; }

        [DataMember]
        public string StatusId { get; set; }

        [DataMember]
        public string GatewayReference { get; set; }

        [DataMember]
        public string GatewayResponse { get; set; }
    }
}
