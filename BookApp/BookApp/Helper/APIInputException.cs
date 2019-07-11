using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace BookApp.Helper {
    /// <summary>
    /// Api Exception
    /// </summary>
    [Serializable]
    [DataContract]
    public class APIInputException : Exception, IAPIException {
        #region Public Serializable properties.
        /// <summary>
        /// Error Code
        /// </summary>
        [DataMember]
        public int ErrorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]

        public string ErrorDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "ApiInputException";
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ReasonPhrase {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }
        #endregion
    }
}