using IdentityServer4.Services;
using IdentityService.Entities;
using IdentityService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    [Route("controller")]
    public class AuthorizationController : ControllerBase
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityServerInteractionService _interaction;

        public AuthorizationController(IIdentityServerInteractionService identityServerInteractionService,
                                      SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _interaction = identityServerInteractionService;
            _signInManager = signInManager;
            _userManager = userManager;
        }



        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(loginModel.Login);

            if(user is null)
            {
                return NotFound();
            }

            var result = await _signInManager.PasswordSignInAsync(user, 
                loginModel.Password, false, false);

            if(result.Succeeded)
            {
                return Redirect(loginModel.ReturnUrl);
            }
            else
            {
                return NotFound();
            }

            
        }

    }
}
