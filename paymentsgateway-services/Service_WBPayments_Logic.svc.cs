using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WBPayments_Logic
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service_WBPayments_Logic : IService_WBPayments_Logic
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Service_WBPayments_Logic));
        private readonly IPaymentManager paymentManager;
        private readonly IAuthManager authManager;

        public Service_WBPayments_Logic(IPaymentManager payment, IAuthManager auth)
        {
            paymentManager = payment;
            authManager = auth;
        }

        public ReturnObject<int> CreatePayment(CreatePaymentObject createPaymentObject)
        {
            ReturnObject<int> response;
            //check if client has role that can create payment
            if (authManager.CanDoOperation(createPaymentObject.ClientId, Constants.OPERATION_PAYMENT_CREATE))
            {
                response = paymentManager.CreatePayment(createPaymentObject, DateTime.Now);
            } else
            {
                log.WarnFormat("The client: {0} tried to access to CreatePayment and does not has access", createPaymentObject.ClientId);
                response = new ReturnObject<int>(0);
                response.addWarnMsg("Access denied");
                response.Code = ReturnObject<int>.RETURN_CODE_UNAUTHORIZED;
            }
            return response;
        }

        public ReturnObject<int> ConfirmPayment(ConfirmPaymentObject confirmPaymentObject)
        {
            ReturnObject<int> response;
            if (authManager.CanDoOperation(confirmPaymentObject.ClientId, Constants.OPERATION_PAYMENT_CONFIRM))
            {
                response = paymentManager.ConfirmPayment(confirmPaymentObject, DateTime.Now);
            }
            else
            {
                log.WarnFormat("The client: {0} tried to access to ConfirmPayment and does not has access", confirmPaymentObject.ClientId);
                response = new ReturnObject<int>(0);
                response.addWarnMsg("Access denied");
                response.Code = ReturnObject<int>.RETURN_CODE_UNAUTHORIZED;
            }
            return response;
        }

        public ReturnObject<bool> ValidateCredentials(String user, String pass)
        {
            ReturnObject<bool> response;
            
            try { 
                //create a 
                response = new ReturnObject<bool>(authManager.HasAccess(user, pass));
                if (!response.Data)
                {
                    log.WarnFormat("Wrong credentials validation user: {0} password {1}", user, pass);
                }
                response.Code = ReturnObject<bool>.RETURN_CODE_OK;
            } catch (Exception e)
            {
                log.ErrorFormat("Exception in ValidateCredentials: {0} for user {1} password {2} message: {3} trace: {4}", e, user, pass, e.Message, e.StackTrace);
                response = new ReturnObject<bool>(false);
                response.Code = ReturnObject<bool>.RETURN_CODE_INTERNAL_ERROR;
            }
            return response;
        }
    }
}
