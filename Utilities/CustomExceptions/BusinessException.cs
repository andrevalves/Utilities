namespace AndiSoft.Utilities.CustomExceptions
{
    public sealed class BusinessException : BaseException
    {
        public BusinessException() : base("BusinessException") { }

        public BusinessException(int code) : base(code, "BusinessException") { }

        public BusinessException(string msg) : base($"BusinessException: {msg}") { }

        public BusinessException(int code, string msg) : base(code, $"BusinessException: {msg}") { }
    }
}