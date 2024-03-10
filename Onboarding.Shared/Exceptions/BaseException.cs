namespace Onboarding.Shared.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BaseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public BaseException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
