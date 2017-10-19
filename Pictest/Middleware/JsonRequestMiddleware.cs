using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pictest.Middleware
{
    public class JsonRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            return this._next(context);
        }
    }
}
