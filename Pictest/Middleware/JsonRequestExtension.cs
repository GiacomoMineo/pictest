using Microsoft.AspNetCore.Builder;

namespace Pictest.Middleware
{
    public static class JsonRequestExtension
    {
        public static IApplicationBuilder UseJsonRequest(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JsonRequestMiddleware>();
        }
    }
}
