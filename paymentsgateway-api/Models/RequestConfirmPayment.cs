using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WBPayments_API.Utils;

namespace WBPayments_API.Models
{
    public class RequestConfirmPayment : RequestBasePayment
    {
        [Required]
        public PaymentResult result { get; set; }        
    }
}