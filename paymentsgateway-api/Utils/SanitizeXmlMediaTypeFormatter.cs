using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security.AntiXss;

namespace WBPayments_API.Utils
{
    public sealed class SanitizeXmlMediaTypeFormatter : XmlMediaTypeFormatter
    {

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
        {
            Task<object> resultTask = base.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);

            var propertiesFlaggedForSanitization = type.GetProperties().Where(e => e.GetCustomAttribute<SanitizeAttribute>() != null).ToList();
            if (propertiesFlaggedForSanitization.Any())
            {
                var result = resultTask.Result;
                foreach (var propertyInfo in propertiesFlaggedForSanitization)
                {
                    var raw = (string)propertyInfo.GetValue(result);
                    if (!string.IsNullOrEmpty(raw))
                    {
                        propertyInfo.SetValue(result, AntiXssEncoder.HtmlEncode(raw, true));
                    }
                }
            }

            return resultTask;
        }
    }
}