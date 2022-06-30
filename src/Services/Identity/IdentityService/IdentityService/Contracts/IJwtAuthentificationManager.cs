using System.Threading.Tasks;

namespace IdentityService.Contracts
{
    public interface IJwtAuthentificationManager
    {
        Task<string> Authentificate(string username, string password); 
    }
}
