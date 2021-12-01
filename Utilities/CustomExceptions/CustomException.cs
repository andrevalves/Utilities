﻿using System;
using System.Runtime.Serialization;
using AndiSoft.Utilities.Extensions;

namespace AndiSoft.Utilities.CustomExceptions
{
    /// <summary>
    /// A custom base class for exceptions with support for Unique IDs (GUID) and Error Codes.
    /// The ErrorID and ErrorCode are also saved in Data["GUID"] and Data["ErrorCode"] for convenience.
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        /// <summary>
        /// Error Code as in Data["ErrorCode"]
        /// </summary>
        public string ErrorCode {
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
                if(Data["GUID"] == null || ((string)Data["GUID"]).IsNullOrEmpty())
                    this.SetExceptionGuid();

                return (string)Data["GUID"];
            }
        }

        /// <summary>
        /// Inicializes a new instance of the CustomException class with serialized data.
        /// </summary>
        public CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Creates a new CustomException. An Unique ID (GUID) will be assigned automatically.
        /// </summary>
        public CustomException()
        {
            this.SetExceptionGuid();
        }

        /// <summary>
        /// Creates a new CustomApplicationException. An Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CustomException(string message) : base(message)
        {
            this.SetExceptionGuid();
        }

        /// <summary>
        /// Creates a new CustomException. An Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        public CustomException(string code, string message) : base(message)
        {
            this.SetExceptionGuid();
            this.SetErrorCode(code);
        }

        /// <summary>
        /// Creates a new CustomException. An Unique ID (GUID) will be assigned automatically.
        /// </summary>
        /// <param name="code">Error code. Will be saved as string</param>
        /// <param name="message">Error message</param>
        public CustomException(long code, string message) : base(message)
        {
            this.SetExceptionGuid();
            this.SetErrorCode(code.ToString());
        }
    }
}