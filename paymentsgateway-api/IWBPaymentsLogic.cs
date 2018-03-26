using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WBPayments_API.Models;

namespace WBPayments_API
{
    public interface IWBPaymentsLogic
    {

        ReturnObject<int> CreatePayment(string clientId, string gatewayId, Payment payment);

        ReturnObject<int> ConfirmPayment(string clientId, int paymentId, PaymentResult result);

        ReturnObject<bool> ValidateCredentials(string user, string pass);

        HttpStatusCode MapResultToHTTPCode(int result);
    }
}
