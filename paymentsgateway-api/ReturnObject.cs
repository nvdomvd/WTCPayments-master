using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace WBPayments_API
{

    public class ReturnObject<T>
    {
       
        private T data;

        List<String> successMsg;
        List<String> warnMsg;
        List<String> errorMsg;
        List<String> infoMsg;

        public ReturnObject(T t, string[] successMsg, string[] warnMsg, string[] errorMsg, string[] infoMsg)
        {
            this.data = t;
            this.successMsg = new List<string>(successMsg);
            this.warnMsg = new List<string>(warnMsg);
            this.errorMsg = new List<string>(errorMsg);
            this.infoMsg = new List<string>(infoMsg);
        }

       
        public List<String> SuccessMsg { get { return successMsg; } }
              
        public List<String> WarnMsg { get { return warnMsg; } }
               
        public List<String> ErrorMsg { get { return errorMsg; } }            
       
        public List<String> InfoMsg { get { return infoMsg; } }
            
        public T Data
        {
            get { return data; }           
        }
       
        public HttpStatusCode HttpCode { get; set; }
    }
}