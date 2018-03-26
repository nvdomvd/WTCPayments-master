using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WBPayments_Logic
{

    [DataContract]
    public class ReturnObject<T>
    {
        [DataMember]
        public const int RETURN_CODE_OK = 1;
        [DataMember]
        public const int RETURN_CODE_CREATED = 2;
        [DataMember]
        public const int RETURN_CODE_BAD_REQUEST = 10;
        [DataMember]
        public const int RETURN_CODE_NOT_FOUND = 11;
        [DataMember]
        public const int RETURN_CODE_UNAUTHORIZED = 12;
        [DataMember]
        public const int RETURN_CODE_PRECONDITION_FAILED = 13;
        [DataMember]
        public const int RETURN_CODE_INTERNAL_ERROR = 14;

        private T data;

        public ReturnObject(T t)
        {
            this.data = t;
            this.SuccessMsg = new List<String>();
            this.WarnMsg = new List<String>();
            this.ErrorMsg = new List<String>();
            this.InfoMsg = new List<String>();
        }

        public void set(T t) { this.data = t; }

        public void addSuccessMsg(String message) { this.SuccessMsg.Add(message); }

        public void addWarnMsg(String message) { this.WarnMsg.Add(message); }

        public void addErrorMsg(String message) { this.ErrorMsg.Add(message); }

        public void addInfoMsg(String message) { this.InfoMsg.Add(message); }

        [DataMember]
        public List<String> SuccessMsg { get; set; }

        [DataMember]
        public List<String> WarnMsg { get; set; }

        [DataMember]
        public List<String> ErrorMsg { get; set; }

        [DataMember]
        public List<String> InfoMsg { get; set; }

        [DataMember]
        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        [DataMember]
        public int Code { get; set; }
    }
}