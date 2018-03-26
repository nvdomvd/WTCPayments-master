using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WBPayments_API.Models;

namespace WBPayments_API.Controllers
{
    public class PaymentsController : ApiController
    {
        private const string TAG_MESSAGE_OK = "OK:";
        private const string TAG_MESSAGE_WARN = "WARN:";
        private const string TAG_MESSAGE_INFO = "INFO:";
        private const string TAG_MESSAGE_ERROR = "ERROR:";
        private const string MESSAGE_SEPARATOR = ",";
        private const string CATEGORY_SEPARATOR = "*";

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IWBPaymentsLogic paymentsLogic;        

        public PaymentsController(IWBPaymentsLogic paymentsLogic)
        {
            this.paymentsLogic = paymentsLogic;            
        }


        private string GetUserId()
        {
            if (User.Identity.IsAuthenticated) { 
                return User.Identity.Name;
            }
            else
            {
                log.Warn("Llego un request sin usuario autenticado");
                return String.Empty;
            }
        }

        // POST: api/Payments
        public IHttpActionResult Post([FromBody]RequestCreatePayment value)
        {
            IHttpActionResult actionResult = null;
            //controlo que llegue el objeto payment
            if (value.payment == null)
            {
                log.Warn("Se intento crear un pago sin enviar el objeto payment");
                actionResult = BadRequest();
            }
            else if (!String.IsNullOrEmpty(GetUserId()))
            {
                try
                {
                    ReturnObject<int> resultado = this.paymentsLogic.CreatePayment(GetUserId(), value.GatewayId, value.payment);
                    //Concateno todos los mensajes recibidos
                    string messages = JoinMessages(resultado);
                    //Mapeo el codigo http calculado a un actionresult
                    actionResult = MapHttpCodeToActionResult(resultado.HttpCode, messages, resultado.Data);
                }
                catch (Exception e)
                {
                    log.Fatal("Ocurrió un error invocando Create Payment", e);
                    actionResult = InternalServerError(e);
                }
            }            
            else
            {
                log.Warn("Se intento crear un pago sin un usuario autenticado");
                actionResult = Unauthorized();
            }

            return actionResult;
        }

        // PUT: api/Payments/5
        public IHttpActionResult Put(int id, [FromBody]RequestConfirmPayment value)
        {
            IHttpActionResult actionResult = null;
            //controlo que llegue el objeto result
            if (value.result == null)
            {
                log.Warn("Se intento confirmar un pago sin enviar el objeto result");
                actionResult = BadRequest();
            } else if (!String.IsNullOrEmpty(GetUserId()))
            {
                try { 
                    ReturnObject<int> resultado = this.paymentsLogic.ConfirmPayment(GetUserId(), id, value.result);
                    //Concateno todos los mensajes recibidos
                    string messages = JoinMessages(resultado);
                    //Mapeo el codigo http calculado a un actionresult
                    actionResult = MapHttpCodeToActionResult(resultado.HttpCode, messages, resultado.Data);
                }
                catch (Exception e)
                {
                    log.Fatal("Ocurrió un error invocando Confirm Payment", e);
                    actionResult = InternalServerError(e);
                }
            }
            else
            {
                log.Warn("Se intento confirmar un pago sin un usuario autenticado");
                actionResult = Unauthorized();
            }
            return actionResult;          
        }


        private IHttpActionResult MapHttpCodeToActionResult(HttpStatusCode httpCode, string message, Object obj)
        {
            IHttpActionResult result = null;            

            switch (httpCode)
            {
                case HttpStatusCode.OK:
                    if (obj != null) { result = Ok(obj); } else { result = Ok(); }                    
                    break;
                case HttpStatusCode.BadRequest:
                    if (String.IsNullOrEmpty(message)) { result = BadRequest(); } else { result = BadRequest(message); }              
                    break;
                case HttpStatusCode.PreconditionFailed:
                    //if we have message, we return badrequest instead of preconditionfailed in order to send the message in the response.
                    if (String.IsNullOrEmpty(message)) { result = StatusCode(httpCode); } else { result = BadRequest(message); }
                    break;
                default: // NotFound, InternalServerError, PreconditionFailed, Unauthorized (por permisos)
                    result = StatusCode(httpCode);                    
                    break;                
            }

            return result;
        }


        private string JoinMessages(ReturnObject<int> resultado)
        {
            string messages = String.Empty;
            if (resultado.SuccessMsg != null && resultado.SuccessMsg.Count > 0)
            {
                messages = TAG_MESSAGE_OK + String.Join(MESSAGE_SEPARATOR, resultado.SuccessMsg) + CATEGORY_SEPARATOR;
            }
            if (resultado.WarnMsg != null && resultado.WarnMsg.Count > 0)
            {
                messages += TAG_MESSAGE_WARN + String.Join(MESSAGE_SEPARATOR, resultado.WarnMsg) + CATEGORY_SEPARATOR;
            }
            if (resultado.ErrorMsg != null && resultado.ErrorMsg.Count > 0)
            {
                messages += TAG_MESSAGE_ERROR + String.Join(MESSAGE_SEPARATOR, resultado.ErrorMsg) + CATEGORY_SEPARATOR;
            }
            if (resultado.InfoMsg != null && resultado.InfoMsg.Count > 0)
            {
                messages += TAG_MESSAGE_INFO + String.Join(MESSAGE_SEPARATOR, resultado.InfoMsg) + CATEGORY_SEPARATOR;
            }

            return messages;
        }
    }
}
