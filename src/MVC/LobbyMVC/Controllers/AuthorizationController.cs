using LobbyMVC.Dtos;
using LobbyMVC.Services.IdentityService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IIdentityService _identityService;

        public AuthorizationController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AuthroizeAsync(AuthentificateUserDto authentificateUserDto)
        {
            var claims = await _identityService.AuthorizeUserAsync(authentificateUserDto);
            return Redirect("/");
        }

    }
}
