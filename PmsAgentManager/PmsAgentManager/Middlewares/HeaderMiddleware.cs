using System.Diagnostics;

namespace PmsAgentManager.Middlewares;

public class HeaderMiddleware
{
    private readonly RequestDelegate _next;

    public HeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(state => {
            var httpContext = (HttpContext)state;
            httpContext.Response.Headers.Add("Content-Type", "text/xml");

            return Task.CompletedTask;
        }, context);

        await _next(context);
    }
}