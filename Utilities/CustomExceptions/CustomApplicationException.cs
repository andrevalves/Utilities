using AndiSoft.Utilities.Extensions;
using System;
using System.Runtime.Serialization;

namespace AndiSoft.Utilities.CustomExceptions
{
    /// <summary>
    /// A custom base class for exceptions with support for Unique IDs (GUID) and Error Codes.
    /// The ErrorID and ErrorCode are also saved in Data["GUID"] and Data["ErrorCode"] for convenience.
    /// </summary>
    [Serializable]
    public class CustomApplicationException : ApplicationException
    {
        /// <summary>
        /// Exception Details.
        /// </summary>
        public string Detail { get; }

        /// <summary>
        /// Error Code as in Data["ErrorCode"]
        /// </summary>
        public string ErrorCode
        {
            get
            {
                if (Data["ErrorCode"] == null || ((string)Data["ErrorCode"]).IsNullOrEmpty())
                    return null;

                return (string)Data["ErrorCode"];
            }
        }

        /// <summary>
        /// Unique Exception ID as in Data["GUID"]
        /// </summary>
        public string ExceptionId
        {
            get
            {
                if (Data["GUID"] == null || ((string)Data["GUID"]).IsNullOrEmpty())
                    this.SetExceptionGuid();

                return (string)Data["GUID"];
            }
        }

        /// <summary>
        /// Inicializes a new instance of the CustomApplicationException class with serialized data.
        /// </summary>
        public CustomApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Creates a new CustomApplicationException. A Unique ID (GUID) will be assigned automatically.
        /// </summary>
        public CustomApplicationException()
        {
            this.SetExceptionGuid();
        }

        /// <summary>
        /// Creates a new CustomApplicationException. A Unique ID (GUID) will be assigned automatically.
        /// </summary>
        public CustomApplicationException(Exception innerException, string detail) : base(innerException.Message, innerException)
        {
            this.SetExceptionGuid();
            Detail = detail;
        }

        /// <summary>
        /// Creates a new CustomApplicationException. A Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner Exception</param>
        /// <param name="detail">Exception Detail</param>
        public CustomApplicationException(string message, Exception innerException = null, string detail = "") : base(message, innerException)
        {
            Detail = detail;
            this.SetExceptionGuid();
        }

        /// <summary>
        /// Creates a new CustomApplicationException. A Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="innerException">Inner Exception</param>
        /// <param name="detail">Exception Detail</param>
        public CustomApplicationException(string code, string message, Exception innerException = null, string detail = "") : base(message, innerException)
        {
            Detail = detail;
            this.SetExceptionGuid();
            this.SetErrorCode(code);
        }

        /// <summary>
        /// Creates a new CustomApplicationException. A Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="code">Error code. Will be saved as string</param>
        /// <param name="message">Error message</param>
        /// <param name="innerException">Inner Exception</param>
        /// <param name="detail">Exception Detail</param>
        public CustomApplicationException(long code, string message, Exception innerException = null, string detail = "") : base(message, innerException)
        {
            Detail = detail;
            this.SetExceptionGuid();
            this.SetErrorCode(code.ToString());
        }
    }
}