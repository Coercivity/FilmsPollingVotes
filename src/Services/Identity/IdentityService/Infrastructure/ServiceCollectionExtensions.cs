using Infrastructure.Repository.Contracts;
using Infrastructure.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IIdentityRepository, IdentityRepository>();

            return services;
        }

    }
}
