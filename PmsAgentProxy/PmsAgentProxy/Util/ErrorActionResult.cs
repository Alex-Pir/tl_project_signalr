using System.Net;
using System.Web.Mvc;

namespace PmsAgentProxy.Util
{
    public class ErrorActionResult : XmlActionResult
    {
        public ErrorActionResult(string document) : base(document)
        {
        }
        
        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}