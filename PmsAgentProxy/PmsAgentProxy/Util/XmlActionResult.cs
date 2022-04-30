using System.Web.Mvc;
using System.Xml;

namespace PmsAgentProxy.Util
{
    public class XmlActionResult : ActionResult
    {
        private readonly string _document;

        private const string MimeType = "text/xml";
        public Formatting Formatting { get; set; }

        public XmlActionResult(string document)
        {
            _document = document;
            Formatting = Formatting.None;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = MimeType;
            context.HttpContext.Response.Write(_document);
        }
    }
}