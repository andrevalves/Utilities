namespace AndiSoft.Utilities.CustomExceptions
{
    /// <summary>
    /// Base class for custom exceptions. Adds a Type description to the exception.
    /// </summary>
    public class BaseException : System.Exception
    {
        public BaseException()
        {
            Data.Add("Type", GetType().ToString());
        }

        public BaseException(string msg) : base(msg)
        {
            Data.Add("Type", GetType().ToString());
        }

        public BaseException(int code)
        {
            Data.Add("Code", code);
            Data.Add("Type", GetType().ToString());
        }

        public BaseException(int code, string msg) : base(msg)
        {
            Data.Add("Code", code);
            Data.Add("Type", GetType().ToString());
        }
    }
}
