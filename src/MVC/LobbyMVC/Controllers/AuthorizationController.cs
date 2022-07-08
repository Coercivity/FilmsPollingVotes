﻿using LobbyMVC.Dtos;
using LobbyMVC.Services.IdentityService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }


        public async Task<IActionResult> AuthroizeAsync(AuthentificateUserDto authentificateUserDto, string returnUrl = "")
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

        [HttpGet("register")]
        public IActionResult Register(string returnUrl = "")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Register");
        }


        public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto)
        {
            await _identityService.RegisterUserAsync(registerUserDto);

            return Redirect("/");
        }
    }
}
