using Application.Service.Interface;
using Application.Service.Interface.Repositories;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Service.Bootstrap
{
    public static class RepositoryRegistrations
    {
        public static IServiceCollection AddRepositorys(this IServiceCollection services)
        {
            return services.AddScoped<IDiffRepository, DiffRepository>();
        }
    }
}
