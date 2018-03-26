using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBPayments_Logic
{
    public class AuthManager : IAuthManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AuthManager));
        private readonly IAuthDao authDao;

        public AuthManager(IAuthDao auth)
        {
            authDao = auth;
        }

        public bool HasAccess(string user, string password)
        {
            try
            {
                return authDao.HasCredentials(user, password);
            }
            catch (Exception e)
            {
                log.ErrorFormat("Exception in HasAccess: {0}, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                //if an exception occurs return false
                return false;
            }
        }

        public bool CanDoOperation(string clientId, string operation)
        {
            try
            {
                //check if the client can access to the feature
                Client clientVerified = authDao.VerifyClientAccess(operation, clientId);
                //if the client is received can, else he cannot
                if (clientVerified != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception e)
            {
                log.ErrorFormat("Exception in CanAccessFeature: {0}, message: {1} trace: {2}", e, e.Message, e.StackTrace);
                //if an exception occurs return false
                return false;
            }
        }
    }
}