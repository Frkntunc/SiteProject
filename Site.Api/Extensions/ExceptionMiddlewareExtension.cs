using Microsoft.AspNetCore.Builder;
using Site.Api.Middlewares;

namespace Site.Api.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        //Yazdığımız exception middleware'i startup'da çağırabilmek için
        public static IApplicationBuilder UseExceptionMiddlaware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
