using Application.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Service.Bootstrap
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<IDiffService, DiffService>();
        }
    }
}
