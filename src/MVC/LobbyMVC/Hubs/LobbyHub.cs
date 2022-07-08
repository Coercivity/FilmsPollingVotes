using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Controllers;
using LobbyMVC.Dtos;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
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
        private readonly IMeetingRepository _meetingRepository;
        private readonly IFilmPollingDataClient _filmPollingDataClient;

        public LobbyHub(IKinopoiskDataClient kinopoiskDataClient, IMeetingRepository meetingRepository,
                        IFilmPollingDataClient filmPollingDataClient, LobbyManager lobbyManager)
        {
            _kinopoiskDataClient = kinopoiskDataClient;
            _meetingRepository = meetingRepository;
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



        public async Task Send(string message, string groupId)
        {

            var film = await _kinopoiskDataClient.GetFilmAttributes(message);

            if(film is null)
            {
                return;
            }

            var userName = Context.User?.Identity?.Name ?? "Anonymous";

            var messageObject = new SignalRMessageObjectDto()
            {
                Film = film,
                User = new User() { Name = userName}
            };


            message = JsonSerializer.Serialize<SignalRMessageObjectDto>(messageObject);

            var groupName = (await _meetingRepository.GetMeetingAsync(Guid.Parse(groupId))).Id;
                
            await Clients.Groups(groupName.ToString()).SendAsync("Send", message);
        }





        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }            



        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

    }
}
