namespace Onboarding.Shared.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message) : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
