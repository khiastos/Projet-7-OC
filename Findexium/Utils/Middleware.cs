using System.Security.Claims;

namespace Findexium.Api.Middleware
{
    public sealed class EndpointAccessLoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public EndpointAccessLoggingMiddleware(ILoggerFactory loggerFactory)
            => _logger = loggerFactory.CreateLogger("AccessLog");

        public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
        {
            var method = ctx.Request.Method;
            var route = (ctx.GetEndpoint() as RouteEndpoint)?.RoutePattern.RawText
                        ?? ctx.Request.Path.ToString();
            var userEmail =
                ctx.User.FindFirstValue(ClaimTypes.Email)
                ?? ctx.User.FindFirstValue("email")
                ?? (ctx.User.Identity?.IsAuthenticated == true ? ctx.User.Identity?.Name : "anonymous");

            _logger.LogInformation("IN  {Method} {Route} by {UserId}", method, route, userEmail);

            try
            {
                await next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EX  {Method} {Route}", method, route);
                throw;
            }

            _logger.LogInformation("OUT {Method} {Route} => {StatusCode} by {UserId}",
                method, route, ctx.Response?.StatusCode, userEmail);
        }
    }
}
