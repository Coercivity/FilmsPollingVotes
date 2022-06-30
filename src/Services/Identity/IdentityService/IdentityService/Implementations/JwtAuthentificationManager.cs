using IdentityService.Contracts;
using Infrastructure.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Implementations
{
    public class JwtAuthentificationManager : IJwtAuthentificationManager
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly string _key;

        public JwtAuthentificationManager(IIdentityRepository identityRepository, IConfiguration configuration)
        {
            _identityRepository = identityRepository;
            _key = configuration.GetSection("Keys:JwtKey").Value;
        }

        public async Task<string> Authentificate(string username, string password)
        {
            var user = await _identityRepository.TryGetUserByLoginAndPasswordAsync(username, password);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
            

        }
    }
}
