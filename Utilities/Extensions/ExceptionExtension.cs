using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AndiSoft.Utilities.Extensions
{
    /// <summary>
    /// Extension methods for Exceptions
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Saves GUID value to Data["GUID"].
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static void SetExceptionGuid(this Exception ex, string guid)
        {
            ex.Data["GUID"] = guid;
        }

        /// <summary>
        /// Gets GUID value in Data["GUID"]. If no value is present, a new GUID is generated and saved in Data["GUID"].
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionGuid(this Exception ex)
        {
            if (ex.Data["GUID"] == null || ((string)ex.Data["GUID"]).IsNullOrEmpty())
                ex.Data["GUID"] = Guid.NewGuid().ToString();

            return (string) ex.Data["GUID"];
        }
        
        /// <summary>
        /// Saves the error code in Data["ErrorCode"].
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static void SetErrorCode(this Exception ex, string code)
        {
            ex.Data["ErrorCode"] = code;
        }

        /// <summary>
        /// Get error code in Data["ErrorCode"].
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetErrorCode(this Exception ex)
        {
            return (string) ex.Data["ErrorCode"];
        }

        private static IEnumerable<dynamic> GetStackTrace(this Exception exception)
        {
            var stackTrace = new StackTrace(exception, true);
            var stackFrames = stackTrace.GetFrames();
            var result = stackFrames?.Select(sf => new
            {
                Caller = (sf.GetMethod() as MethodInfo)?.ToShortString() ?? string.Empty,
                FileName = Path.GetFileName(sf.GetFileName()),
                LineNumber = sf.GetFileLineNumber(),
            });

            return result;
        }
        
        /// <summary>
        /// List inner exceptions of an exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="includeCurrent"></param>
        /// <returns></returns>
        public static IEnumerable<Node<Exception>> GetInnerExceptions(this Exception exception, bool includeCurrent = true)
        {
            if (exception == null) { throw new ArgumentNullException(nameof(exception)); }

            var exceptionStack = new Stack<Node<Exception>>();

            var depth = 0;

            if (includeCurrent)
            {
                exceptionStack.Push(new Node<Exception>(exception, depth));
            }

            while (exceptionStack.Any())
            {
                var current = exceptionStack.Pop();
                yield return current;

                if (current.Value is AggregateException)
                {
                    depth++;
                    foreach (var innerException in ((AggregateException)current).InnerExceptions)
                    {
                        exceptionStack.Push(new Node<Exception>(innerException, depth + 1));
                    }
                    continue;
                }
                if (current.Value.InnerException != null)
                {
                    depth++;
                    exceptionStack.Push(new Node<Exception>(current.Value.InnerException, depth));
                    depth--;
                }
            }
        }

        public class Node<T>
        {
            public Node(T value, int depth)
            {
                Value = value;
                Depth = depth;
            }
            public T Value { get; }
            public int Depth { get; }

            public static implicit operator T(Node<T> node) => node.Value;
        }
    }
}