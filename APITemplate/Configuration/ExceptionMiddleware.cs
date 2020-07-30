using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Levinor.APITemplate.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger logger)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex.ToString());
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsync(ex.Message);
            }
            finally
            {
                RouteData route = httpContext.GetRouteData();
                if (route != null && route.Values != null) logger.LogError((route.Values.TryGetValue("action", out object action)) ? action.ToString() : "Unknown");
            }
        }
    }
}
