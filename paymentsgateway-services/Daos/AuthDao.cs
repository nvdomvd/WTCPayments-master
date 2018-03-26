using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBPayments_Logic
{
    public class AuthDao : IAuthDao
    {
        public bool HasCredentials(string user, string password)
        {
            using (var db = new wbpaymentsEntities())
            {
                //get the client password with client_id = user and date_until not null
                var L2EQuery = from cp in db.ClientPassword
                               where cp.client_id == user
                               orderby cp.date_created descending
                               select cp;
                var clientPassword = L2EQuery.FirstOrDefault<ClientPassword>();
                if (clientPassword != null) { 
                    //check if the password received verify the one that is in DB
                    return BCrypt.Net.BCrypt.Verify(password, clientPassword.password);
                } else
                {
                    //if not found clientpassword, always return false
                    return false;
                }
            }
        }
        
        public Client VerifyClientAccess(string operationId, string clientId)
        {
            using (var db = new wbpaymentsEntities())
            {
                //check if the client has a role that can perform an operation with operationId
                var clients = db.Operation
                    .Where(o => o.operation_id == operationId)
                    .SelectMany(o => o.Role.SelectMany(r => r.Client.Where(c => c.client_id == clientId)))
                    .Distinct()
                    .ToList();
                //return the getted client
                return clients.FirstOrDefault<Client>();
            }
        }
    }
}