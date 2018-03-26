using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WBPayments_API.Utils;

namespace WBPayments_API.Models
{
    public class PaymentResult
    {
        [Sanitize]
        public string GatewayReference { get; set; }

        [Sanitize]
        public string GatewayResponse { get; set; }

        [Required]
        [Sanitize]
        public string StatusId { get; set; }        

    }
}