using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WBPayments_API.Utils;

namespace WBPayments_API.Models
{
    public class RequestCreatePayment : RequestBasePayment
    {

        [SanitizeAttribute]
        [Required]
        public string GatewayId { get; set; }

        public Payment payment { get; set; }
    }
}