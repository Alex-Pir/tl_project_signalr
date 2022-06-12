using Microsoft.AspNetCore.Mvc.Filters;

namespace PmsAgentManager.Attributes;

public class XmlHeaderAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.ContentType = "text/xml; charset=utf-8";
    }
}