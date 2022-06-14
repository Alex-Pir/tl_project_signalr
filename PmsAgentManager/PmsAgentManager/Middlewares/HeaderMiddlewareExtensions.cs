namespace PmsAgentManager.Middlewares;

public static class HeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseHeader(this IApplicationBuilder app)  
    {  
        return app.UseMiddleware<HeaderMiddleware>();  
    }  
}
