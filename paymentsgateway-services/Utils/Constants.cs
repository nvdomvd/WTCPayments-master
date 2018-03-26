using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBPayments_Logic
{
    public static class Constants
    {
        public static string OPERATION_PAYMENT_CREATE = "CREATE_PAYMENT";
        public static string OPERATION_PAYMENT_CONFIRM = "CONFIRM_PAYMENT";
        public static string PAYMENTS_INVOICE_MAX = "MAX_PAYMENTS_INVOICE";
        public static int PAYMENTS_INVOICE_INFINITY = -1;
        public static string PAYMENT_STATUS_NEW_ID = "STATUS_NEW";
        public static string PAYMENT_STATUS_APPROVED_ID = "STATUS_APPROVED";
    }
}