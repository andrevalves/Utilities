namespace AndiSoft.Utilities.CustomExceptions
{
    public sealed class ValidationException : BaseException
    {
        public ValidationException() : base("ValidationException") { }

        public ValidationException(int code) : base(code, "ValidationException") { }

        public ValidationException(string msg) : base($"ValidationException: {msg}") { }

        public ValidationException(int code, string msg) : base(code, $"ValidationException: {msg}") { }
    }
}