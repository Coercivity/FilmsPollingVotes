using Domain.Entities;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyMVC.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly IKinopoiskDataClient _kinopoiskDataClient;

        public LobbyHub(IKinopoiskDataClient kinopoiskDataClient)
        {
            _kinopoiskDataClient = kinopoiskDataClient;
        }

        public async Task Send(string message)
        {
            var film = await _kinopoiskDataClient.GetFilmAttributes(message);

            message = JsonSerializer.Serialize<Film>(film);


            await Clients.All.SendAsync("Send", message);
        }


    }
}
