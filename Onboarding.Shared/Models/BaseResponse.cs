namespace Onboarding.Shared.Models
{
    public record BaseResponse<T>
    {
        public string ResponseCode { get; set; } = ResponseCodes.Ok;
        public string ResponseMessage { get; set; } = ResponseMessages.Ok;
        public T ResponseData { get; set; }
    }
}
