using Application.Contracts;
using Domain.Entities;
using LobbyMVC.FilmPollingDataService;
using LobbyMVC.Helpers;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {


        private readonly IMeetingRepository _meetingRepository;
        private readonly IKinopoiskDataClient _kinopoiskDataClient;
        private readonly IFilmPollingDataClient _filmPollingDataClient;

        public LobbyController(IMeetingRepository meetingRepository, IKinopoiskDataClient kinopoiskDataClient,
                               IFilmPollingDataClient filmPollingDataClient)
        {
            _meetingRepository = meetingRepository;
            _kinopoiskDataClient = kinopoiskDataClient;
            _filmPollingDataClient = filmPollingDataClient;
        }

        [Route("meeting/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {

            var meeting = await _meetingRepository.GetMeetingAsync(id);

            return View(meeting);
        }



        public async Task<IActionResult> CreateLobby(string lobbyName)
        {   

            var id = Guid.NewGuid();

            var link = UriHelper.BuildLink(id, Url.Action(nameof(Index), "meeting"));

            var meeting = new Meeting()
            {
                Id = id,
                Name = lobbyName,
                URL = link

            };

            await _meetingRepository.CreateMeetingAsync(meeting);

            return RedirectToAction(nameof(Index), new { id = meeting.Id });
        }



    }
}
