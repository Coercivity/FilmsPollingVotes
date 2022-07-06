using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Controllers;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace LobbyMVC.Hubs
{
    [Authorize]
    public class LobbyHub : Hub
    {
        private readonly IKinopoiskDataClient _kinopoiskDataClient;
        private readonly IMeetingRepository _meetingRepository;

        private  Meeting _meeting { get; set; } 

    
        

        public LobbyHub(IKinopoiskDataClient kinopoiskDataClient, IMeetingRepository meetingRepository)
        {
            _kinopoiskDataClient = kinopoiskDataClient;
            _meetingRepository = meetingRepository;
        }

        public override async Task OnConnectedAsync()
        {

             await Clients.Client(Context.ConnectionId).SendAsync("OnConnect", Context.ConnectionId);


             await base.OnConnectedAsync();
        }

        public async Task Send(string message, string groupId)
        {
 

            var film = await _kinopoiskDataClient.GetFilmAttributes(message);

            message = JsonSerializer.Serialize<Film>(film);

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
