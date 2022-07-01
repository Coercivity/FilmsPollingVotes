using Domain.Entities;
using LobbyMVC.Dtos;
using LobbyMVC.KinopoiskDataService;
using LobbyMVC.Services.IdentityService;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyMVC.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly IKinopoiskDataClient _kinopoiskDataClient;
        private readonly IIdentityService _identityService;

        public LobbyHub(IKinopoiskDataClient kinopoiskDataClient, IIdentityService identityService)
        {
            _kinopoiskDataClient = kinopoiskDataClient;
            _identityService = identityService;
        }

        public async Task Send(string message)
        {

            var authentificateUserDto = new AuthentificateUserDto()
            {
                UserName = "ass",
                Password = "qwert"
            };
            var claims = await _identityService.AuthorizeUserAsync(authentificateUserDto);

            var film = await _kinopoiskDataClient.GetFilmAttributes(message);

            message = JsonSerializer.Serialize<Film>(film);


            await Clients.All.SendAsync("Send", message);
        }


    }
}
