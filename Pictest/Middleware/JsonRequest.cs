using Microsoft.AspNetCore.Builder;

namespace Pictest.Middleware
{
    public static class JsonRequestExtensions
    {
        public static IApplicationBuilder UseJsonRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JsonRequestMiddleware>();
        }
    }
}
