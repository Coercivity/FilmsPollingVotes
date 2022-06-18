using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LobbyMVC.KinopoiskDataService
{

    public class KinopoiskDataClient : IKinopoiskDataClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        private string _url;
        private readonly string _urlParameters;
        private readonly string _token;
        private Film _film;

        public KinopoiskDataClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

            _url = _configuration.GetSection("KinopoiskAPISettings:url").Value;
            _urlParameters = _configuration.GetSection("KinopoiskAPISettings:urlParameters").Value;
            _token = _configuration.GetSection("KinopoiskAPISettings:token").Value;
        }

        public  async Task<Film> GetFilmAttributes(string link)
        {
            _url += link;


            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_url);


            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _urlParameters);
            httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", _token);
            httpClient.DefaultRequestHeaders.Accept.Add(
                          new MediaTypeWithQualityHeaderValue("application/json"));


            var response =  httpClient.GetAsync(_url).Result;

            

            if (response.IsSuccessStatusCode)
            {
                _film = response.Content.ReadAsAsync<Film>().Result;
                return _film;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return null;

        }
    }
}
