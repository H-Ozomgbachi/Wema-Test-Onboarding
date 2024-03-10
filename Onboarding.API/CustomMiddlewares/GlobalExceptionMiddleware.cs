namespace Onboarding.API.CustomMiddlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            _logger.LogError($"Error Processing Request\nMessage: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");

            httpContext.Response.ContentType = GeneralConstants.ApplicationJson;
            httpContext.Response.StatusCode = (int)GetStatusCode(ex);

            string errorMessage = ex.InnerException?.Message ?? ex.Message;

            BaseResponse<object> response = new();

            if (ex is BaseException)
            {
                BaseException baseException = ex as BaseException;
                (string code, string message) = GetResponseCode(baseException?.StatusCode ?? HttpStatusCode.InternalServerError);

                response.ResponseCode = code;
                response.ResponseMessage = message;
                response.ResponseData = baseException?.Message ?? ResponseMessages.InternalServer;
            }
            else
            {
                (string code, string message) = GetResponseCode(HttpStatusCode.InternalServerError);

                response.ResponseCode = code;
                response.ResponseMessage = message;
                response.ResponseData = errorMessage;
            }

            JsonSerializerSettings options = new() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, options));
        }

        private static HttpStatusCode GetStatusCode(Exception ex)
        {
            if (ex is not BaseException internalException)
            {
                return HttpStatusCode.InternalServerError;
            }
            return internalException.StatusCode;
        }
        private static (string code, string message) GetResponseCode(HttpStatusCode statusCode)
        {
            (string code, string message) = (int)statusCode switch
            {
                400 => (ResponseCodes.BadRequest, ResponseMessages.BadRequest),
                404 => (ResponseCodes.NotFound, ResponseMessages.NotFound),
                _ => (ResponseCodes.InternalServer, ResponseMessages.InternalServer),
            };
            return (code, message);
        }
    }
}
