using Application.Contracts;
using Domain.Entities;
using LobbyMVC.Dtos;
using LobbyMVC.KinopoiskDataService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    public class LobbyController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IKinopoiskDataClient _kinopoiskDataClient;

        public LobbyController(IMeetingRepository meetingRepository, IKinopoiskDataClient kinopoiskDataClient)
        {
            _meetingRepository = meetingRepository;
            _kinopoiskDataClient = kinopoiskDataClient;
        }

        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Create(string link)
        {
            var film = await _kinopoiskDataClient.GetFilmAttributes(link);

            return View("index", film);
        }


    }
}
