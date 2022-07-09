using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Controllers;
using LobbyMVC.Dtos;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.Helpers;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyMVC.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {

        private readonly LobbyManager _lobbyManager;

        private readonly IKinopoiskDataClient _kinopoiskDataClient;
        private readonly IFilmPollingDataClient _filmPollingDataClient;

        public LobbyHub(IKinopoiskDataClient kinopoiskDataClient, IFilmPollingDataClient filmPollingDataClient, 
            LobbyManager lobbyManager)
        {
            _kinopoiskDataClient = kinopoiskDataClient;
            _filmPollingDataClient = filmPollingDataClient;         
            _lobbyManager = lobbyManager;
        }


        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? "Anonymous";
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("OnConnect", connectionId);
            _lobbyManager.ConnectUser(userName, connectionId);

            await base.OnConnectedAsync();

        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            await Clients.Client(connectionId).SendAsync("OnDisconnect", connectionId);
            _lobbyManager.DisconnectUser(connectionId);


            await base.OnDisconnectedAsync(exception);
        }


        private async Task<Film> GetFilmByUserInput(string message, string groupId)
        {
            var film = await _kinopoiskDataClient.GetFilmAttributes(message);

            if (film is null)
            {
                return null;
            }

            film.LobbyId = Guid.Parse(groupId);
            film.CreatorId = Guid.Parse(Context.User.FindFirst("Id").Value);
            film.Id = Guid.NewGuid();

            return film;
        }


        public async Task AddItem(string message, string groupId)
        {

            if(_lobbyManager.CheckIfKinopoiskIdExistsInGroup(message, groupId, out string filmId))
            {
                //already exists
                //need to implement pop up window with warning
                return;
            }

            var film = await GetFilmByUserInput(filmId, groupId);

            if(film is null)
            {
                return;
            }

            var pollingModel = new PollingModel()
            { 
                CreatorId = film.CreatorId,
                CreatorWeight = 1.0f,
                EntityId = film.Id,
                MeetingId = Guid.Parse(groupId)
            };
            await _filmPollingDataClient.AddFilmAsync(pollingModel);


            var messageObject = new SignalRMessageObject()
            {
                Film = film,
                User = new User()
                {
                    Name = Context.User?.Identity?.Name ?? "Anonymous",
                    Id = film.CreatorId,
                    CreatorWeight = 1
                }
            };
            _lobbyManager.HubCache[groupId].Add(messageObject);
            message = JsonSerializer.Serialize<SignalRMessageObject>(messageObject);
            await Clients.Groups(groupId).SendAsync("AddItem", message);
        }



        public async Task UpdateItems(string groupId, bool forEveryone = false)
        {
            var films = _lobbyManager.HubCache[groupId];
            var message = JsonSerializer.Serialize<List<SignalRMessageObject>>(films);
            if (forEveryone)
            {
                await Clients.Groups(groupId).SendAsync("UpdateItems", message);
            }
            else
            {
                await Clients.Caller.SendAsync("UpdateItems", message);
            }
        }

        public async Task RemoveItem(string itemId, string groupId)
        {
            var itemToDelete = _lobbyManager.HubCache[groupId].Where(x => x.Film.Id.Equals(Guid.Parse(itemId))).FirstOrDefault();
            _lobbyManager.HubCache[groupId].Remove(itemToDelete);
            await _filmPollingDataClient.RemoveFilmByIdAsync(Guid.Parse(itemId), Guid.Parse(groupId));
            await UpdateItems(groupId, true);
        }

        public async Task AddToGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
            if (!_lobbyManager.HubCache.ContainsKey(groupId))
            {
                _lobbyManager.HubCache.Add(groupId, new List<SignalRMessageObject>());
            }
            await Clients.Groups(groupId).SendAsync("AddUser", Context.User.Identity.Name);
        }            

        public async Task RemoveFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

    }
}
