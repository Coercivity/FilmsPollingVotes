using Application.Contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbRepositories(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<LobbyDbContext>(op => op.UseSqlServer(connectionString));


            services.AddTransient<IMeetingRepository, MeetingRepository>();
            services.AddTransient<IFilmRepository, FilmRepository>();
            services.AddTransient<IUserRepository, UserRepository>();



            return services;
        }
    }
}