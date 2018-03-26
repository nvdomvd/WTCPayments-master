using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPayments_Logic
{
    public interface IPaymentManager
    {
        ReturnObject<int> ConfirmPayment(ConfirmPaymentObject confirmPaymentObject, DateTime date);

        ReturnObject<int> CreatePayment(CreatePaymentObject createPaymentObject, DateTime date);
    }
}
