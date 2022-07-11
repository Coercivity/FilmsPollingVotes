using Domain.Entities;
using LobbyMVC.Dtos;
using LobbyMVC.FilmPollingDataService;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IFilmPollingDataClient _filmPollingDataClient;

        public LobbyHub(IFilmPollingDataClient filmPollingDataClient, 
            LobbyManager lobbyManager)
        {
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


        public async Task AddItem(string message, string groupId)
        {

            if(_lobbyManager.CheckIfKinopoiskIdExistsInGroup(message, groupId, out string filmId))
            {
                //already exists
                //need to implement pop up window with warning
                return;
            }

            var userId = Context.User.FindFirst("Id").Value;

            var film = await _lobbyManager.GetFilmByUserInput(filmId, groupId, userId);

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
                    Id = Guid.Parse(userId),
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
                await Clients.Groups(groupId).SendAsync("UpdateAllItems", message);
            }
            else
            {
                await Clients.Caller.SendAsync("UpdateItemsForNewConnection", message);
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


        public async Task GetLobbyWinner(string groupId)
        {
            var winnerId = await _filmPollingDataClient.GetWinnerByLobbyIdAsync(Guid.Parse(groupId));
            var lobbyFilms = await _filmPollingDataClient.GetFilmsByLobbyIdAsync(Guid.Parse(groupId));

            var winner = _lobbyManager.HubCache[groupId].Where(x => x.Film.Id.Equals(winnerId)).FirstOrDefault();
            var serializedWinner = JsonSerializer.Serialize<SignalRMessageObject>(winner);
            await Clients.Groups(groupId).SendAsync("PresentWinner", serializedWinner, lobbyFilms);
        }


        public async Task RemoveFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

    }
}
