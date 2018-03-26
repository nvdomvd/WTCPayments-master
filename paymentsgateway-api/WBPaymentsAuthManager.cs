using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace WBPayments_API.Modules
{
    
        public class BasicAuthHttpModule : IHttpModule
        {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IWBPaymentsLogic paymentsLogic;

        public BasicAuthHttpModule(IWBPaymentsLogic paymentsLogic)
        {
            this.paymentsLogic = paymentsLogic;
        }

        public void Init(HttpApplication context)
            {
                // Register event handlers
                context.AuthenticateRequest += OnApplicationAuthenticateRequest;
                context.EndRequest += OnApplicationEndRequest;
            
            }

            private static void SetPrincipal(IPrincipal principal)
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }

            
            private bool CheckPassword(string username, string password)
            {

                try { 
                    ReturnObject<bool> isvalid = paymentsLogic.ValidateCredentials(username, password);
                    return isvalid.Data;
                }
                catch (Exception e)
                {
                    log.Fatal("Ocurrió un error invocando Validate Credentials", e);
                    return false;
                }

        }

            private void AuthenticateUser(string credentials)
            {
                try
                {
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    credentials = encoding.GetString(Convert.FromBase64String(credentials));

                    int separator = credentials.IndexOf(':');
                    string name = credentials.Substring(0, separator);
                    string password = credentials.Substring(separator + 1);

                    if (CheckPassword(name, password))
                    {
                        var identity = new GenericIdentity(name);
                        SetPrincipal(new GenericPrincipal(identity, null));
                    }
                    else
                    {
                        // Invalid username or password.
                    
                        HttpContext.Current.Response.StatusCode = 401;                                            
                    }
                }
                catch (FormatException)
                {
                    // Credentials were not formatted correctly.
                    HttpContext.Current.Response.StatusCode = 401;                                       
                }
            }

            private void OnApplicationAuthenticateRequest(object sender, EventArgs e)
            {
                var request = HttpContext.Current.Request;
                //TODO MSACCONE: como lo mapeo segun el parametro?
                //HttpContext.Current.Response.ContentType = "application/json";
                var authHeader = request.Headers["Authorization"];
                if (authHeader != null)
                {
                    var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                    // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                    if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                        authHeaderVal.Parameter != null)
                    {
                        AuthenticateUser(authHeaderVal.Parameter);
                    }
                }
            }

            // If the request was unauthorized, add the WWW-Authenticate header 
            // to the response.
            private void OnApplicationEndRequest(object sender, EventArgs e)
            {
                var response = HttpContext.Current.Response;

                
                if (response.StatusCode == 401)
                {
                    response.ClearContent();
                    response.Write("Access denied. Invalid credentials");
                //response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", Realm));
            }
            }

            public void Dispose()
            {
            }
        }
    
}