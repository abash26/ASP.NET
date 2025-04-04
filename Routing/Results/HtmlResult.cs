using System.Text;

namespace Routing.Results
{
    public class HtmlResult(string html) : IResult
    {
        private readonly string html = html;

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "text/html";
            httpContext.Response.ContentLength = Encoding.UTF8.GetByteCount(html);

            await httpContext.Response.WriteAsync(html);
        }
    }
}
