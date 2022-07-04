using LobbyMVC.Dtos;
using LobbyMVC.Services.IdentityService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LobbyMVC.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IJWTIdentityService _identityService;

        public AuthorizationController(IJWTIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AuthroizeAsync(AuthentificateUserDto authentificateUserDto)
        {
            var claimsPrincipal = await _identityService.AuthorizeUserAsync(authentificateUserDto);

            await HttpContext.SignInAsync(claimsPrincipal);



            return Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


    }
}
