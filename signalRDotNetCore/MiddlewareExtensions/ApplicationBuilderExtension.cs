using Microsoft.AspNetCore.Builder;
using signalRDotNetCore.Classes;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace signalRDotNetCore.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
           where T : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var services = serviceProvider.GetService<T>();
            services.SubscribeProductTableDependency(connectionString);
        }
    }
}