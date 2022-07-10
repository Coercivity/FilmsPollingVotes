using LobbyMVC.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LobbyMVC.Services.IdentityService
{
    public interface IJWTIdentityService
    {
        Task<ClaimsPrincipal> AuthorizeUserAsync(AuthentificateUserDto authentificateUserDto);

        Task<RegistrationStatus> RegisterUserAsync(RegisterUserDto user);
    }

    public enum RegistrationStatus
    {
        Success = 1,
        AccountExists,
        Error
    }


}
