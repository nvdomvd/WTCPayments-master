//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace WBPayments_Logic
{
    using System;
    using System.Collections.Generic;
    
    public partial class GatewayPaymentData
    {
        public int payment_id { get; set; }
        [Required, MaxLength(50)]
        public string gateway_reference { get; set; }
        [Required, MaxLength(2000)]
        public string gateway_response { get; set; }
        public System.DateTime date_created { get; set; }
    
        public virtual Payment Payment { get; set; }
    }
}
