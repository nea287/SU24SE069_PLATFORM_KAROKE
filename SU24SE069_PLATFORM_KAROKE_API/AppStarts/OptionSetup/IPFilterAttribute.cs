using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup
{
    public class IPFilterAttribute : ActionFilterAttribute
    {
        private readonly string _allowedIPAddress;

        public IPFilterAttribute(string allowedIPAddress)
        {
            _allowedIPAddress = allowedIPAddress;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestIP = context.HttpContext.Connection.RemoteIpAddress;
            if (requestIP!.ToString() != _allowedIPAddress)
            {
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "Forbidden: Access is denied."
                };
            }
            base.OnActionExecuting(context);
        }
    }
}
