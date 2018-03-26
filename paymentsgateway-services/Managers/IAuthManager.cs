using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPayments_Logic
{
    public interface IAuthManager
    {
        bool HasAccess(string user, string pass);

        bool CanDoOperation(string clientId, string operation);
    }
}
