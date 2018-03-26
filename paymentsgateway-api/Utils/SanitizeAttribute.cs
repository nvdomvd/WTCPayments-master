using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBPayments_API.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SanitizeAttribute : Attribute
    {
    }
}