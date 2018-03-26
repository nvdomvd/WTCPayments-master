using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPayments_Logic
{
    public interface IAuthDao
    {
        bool HasCredentials(string user, string password);
        Client VerifyClientAccess(string operationId, string clientId);
    }
}
