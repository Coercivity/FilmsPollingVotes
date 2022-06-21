using LobbyMVC.FilmPollingDataService;
using LobbyMVC.KinopoiskDataService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace LobbyMVC.Extensions
{
    public static class HttpClientServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {

            //is it better to use Singleton instead?

            services.AddHttpClient<IKinopoiskDataClient, KinopoiskDataClient>(client => 
            { 
                client.BaseAddress = new Uri(configuration.GetSection("KinopoiskAPISettings:url").Value);

                client.DefaultRequestHeaders.Add("X-API-KEY", configuration.GetSection("KinopoiskAPISettings:urlParameters").Value);
                client.DefaultRequestHeaders.Add("Bearer", configuration.GetSection("KinopoiskAPISettings:token").Value);
                client.DefaultRequestHeaders.Accept
                         .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            });



            services.AddHttpClient<IFilmPollingDataClient, FilmPollingDataClient>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("FilmPollingAPISettings:url").Value);

                client.DefaultRequestHeaders.Accept
                         .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            });



            return services;
        }
    }
}
