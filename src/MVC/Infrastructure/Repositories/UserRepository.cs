using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LobbyDbContext _lobbyDbContext;

        public UserRepository(LobbyDbContext lobbyDbContext)
        {
            _lobbyDbContext = lobbyDbContext;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _lobbyDbContext.Users.Where(usr => usr.Id.Equals(id)).FirstOrDefaultAsync();

            if(user is null)
            {
                return null;
            }
            return user;
        }
    }
}
