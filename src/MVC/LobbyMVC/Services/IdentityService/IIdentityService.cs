using Domain.Entities;
using LobbyMVC.Dtos;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LobbyMVC.Services.IdentityService
{
    public interface IJWTIdentityService
    {
        Task<ClaimsPrincipal> AuthorizeUserAsync(AuthentificateUserDto authentificateUserDto);

        Task RegisterUserAsync(User user);
    }
}
