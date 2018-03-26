using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WBPayments_API.Models;

namespace WBPayments_API
{
    public class WBPaymentsLogic : IWBPaymentsLogic

    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ReturnObject<int> ConfirmPayment(string clientId, int paymentId, PaymentResult paymentResult)
        {
            log.Info("ConfirmPayment");
            Service_WBPayments_Reference.Service_WBPayments_LogicClient service = new Service_WBPayments_Reference.Service_WBPayments_LogicClient();
            Service_WBPayments_Reference.ConfirmPaymentObject confirmRequest = new Service_WBPayments_Reference.ConfirmPaymentObject();

            confirmRequest.ClientId = clientId;
            confirmRequest.PaymentId = paymentId;
            confirmRequest.GatewayReference = paymentResult.GatewayReference;
            confirmRequest.GatewayResponse = paymentResult.GatewayResponse;
            confirmRequest.StatusId = paymentResult.StatusId;

            Service_WBPayments_Reference.ReturnObjectOfint intResult = service.ConfirmPayment(confirmRequest);
            ReturnObject<int> result = new ReturnObject<int>(intResult.Data, intResult.SuccessMsg, intResult.WarnMsg, intResult.ErrorMsg, intResult.InfoMsg);
            result.HttpCode = MapResultToHTTPCode(intResult.Code);

            return result;
        }

        public ReturnObject<int> CreatePayment(string clientId, string gatewayId, Payment payment)
        {
            log.Info("CreatePayment");

            Service_WBPayments_Reference.Service_WBPayments_LogicClient service = new Service_WBPayments_Reference.Service_WBPayments_LogicClient();

            Service_WBPayments_Reference.CreatePaymentObject createRequest = new Service_WBPayments_Reference.CreatePaymentObject();

            createRequest.ClientId = clientId;
            createRequest.GatewayId = gatewayId;
            createRequest.Amount = payment.Amount;
            createRequest.Comment = payment.Comment;
            createRequest.CurrencyId = payment.CurrencyId;
            createRequest.InvoiceNumber = payment.InvoiceReference;                

            Service_WBPayments_Reference.ReturnObjectOfint intResult = service.CreatePayment(createRequest);
            ReturnObject<int> result = new ReturnObject<int>(intResult.Data, intResult.SuccessMsg, intResult.WarnMsg, intResult.ErrorMsg, intResult.InfoMsg);
            result.HttpCode = MapResultToHTTPCode(intResult.Code);

            return result;
        }


        public ReturnObject<bool> ValidateCredentials(string user, string pass)
        {
            log.Info("ValidateCredentials");

            ReturnObject<bool> result;
            Service_WBPayments_Reference.Service_WBPayments_LogicClient service = new Service_WBPayments_Reference.Service_WBPayments_LogicClient();

            Service_WBPayments_Reference.ReturnObjectOfboolean boolResult = service.ValidateCredentials(user, pass);

            result = new ReturnObject<bool>(boolResult.Data, boolResult.SuccessMsg, boolResult.WarnMsg, boolResult.ErrorMsg, boolResult.InfoMsg);

            result.HttpCode = MapResultToHTTPCode(boolResult.Code);

            return result;
            
        }

        private const int RETURN_CODE_OK = 1;
        private const int RETURN_CODE_CREATED = 2;
        private const int RETURN_CODE_BAD_REQUEST = 10;
        private const int RETURN_CODE_NOT_FOUND = 11;
        private const int RETURN_CODE_UNAUTHORIZED = 12;
        private const int RETURN_CODE_PRECONDITION_FAILED = 13;
        private const int RETURN_CODE_INTERNAL_ERROR = 14;


        public HttpStatusCode MapResultToHTTPCode(int result)
        {
            HttpStatusCode httpCode;
            switch (result)
            {
                case RETURN_CODE_OK:
                    httpCode = HttpStatusCode.OK;
                    break;
                    //MSACCONE: No diferencio entre OK, y CREATED; en ambos casos retorno lo mismo.
                case RETURN_CODE_CREATED:
                    httpCode = HttpStatusCode.OK;
                    break;
                case RETURN_CODE_UNAUTHORIZED:
                    httpCode = HttpStatusCode.Unauthorized;
                    break;
                case RETURN_CODE_BAD_REQUEST:
                    httpCode = HttpStatusCode.BadRequest;
                    break;
                case RETURN_CODE_NOT_FOUND:
                    httpCode = HttpStatusCode.NotFound;
                    break;
                case RETURN_CODE_PRECONDITION_FAILED:
                    httpCode = HttpStatusCode.PreconditionFailed;
                    break;
                case RETURN_CODE_INTERNAL_ERROR:
                    httpCode = HttpStatusCode.InternalServerError;
                    break;
                default:
                    //TODO MSACCONE: Confirmar este ultimo resultado en caso de recibir un codigo no previsto.
                    log.Error("El codigo recibido por el servicio no esta contemplado. CODIGO=" + result);
                    httpCode = HttpStatusCode.NotImplemented;
                    break;
            }

            return httpCode;
        }
    }
}