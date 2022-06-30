namespace IdentityService.Contracts
{
    public interface IJwtAuthentificationManager
    {
        string Authentificate(string username, string password); 
    }
}
