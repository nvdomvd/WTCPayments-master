using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WBPayments_API.Utils;

namespace WBPayments_API.Models
{
    public class Payment
    {

        [Required]
        public decimal Amount { get; set; }
        [SanitizeAttribute]
        public string InvoiceReference { get; set; }

        [SanitizeAttribute]
        public string CurrencyId { get; set; }
        
        [SanitizeAttribute]
        public string Comment { get; set; }

    }
}