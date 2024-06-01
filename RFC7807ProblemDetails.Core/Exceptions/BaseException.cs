namespace RFC7807ProblemDetails.Core.Exceptions
{
    public class BaseException : Exception
    {
        public int ErrorCode { get; }

        public BaseException(int errorCode, Exception innerException) : base(GetErrorMessage(errorCode), innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
