using Business;
using Microsoft.Extensions.DependencyInjection;

namespace QualityData.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient<IProducto, ProductoBusiness>();
            return services;
        }
    }
}
