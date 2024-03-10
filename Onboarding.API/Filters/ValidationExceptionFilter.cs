namespace Onboarding.API.Filters
{
    public class ValidationExceptionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.ContentType = GeneralConstants.ApplicationJson;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                BaseResponse<object> result = new()
                {
                    ResponseCode = ResponseCodes.BadRequest,
                    ResponseMessage = "One or more validation errors occurred.",
                    ResponseData = errors
                };

                context.Result = new JsonResult(result);
            }
        }
    }
}
