using Domain.Entities;
using Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Implementations
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityDbContext _identityDbContext;

        public IdentityRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }

        public async Task<User> TryGetUserByLoginAndPasswordAsync(string login, string password)
        {
            var user = await _identityDbContext.Users.Where(u => u.Password.Equals(password) 
                                                        && u.UserName.Equals(login)).FirstOrDefaultAsync();
            if(user is null)
            {
                return null;
            }

            return user; 

        }

        public async Task<RegistrationStatus> TryRegisterAsync(User user)
        {
            var exists = await _identityDbContext.Users.AnyAsync(u => u.UserName.Equals(user.UserName));

            if(exists)
            {
                return RegistrationStatus.AccountExists;
            }
            try
            {
                await _identityDbContext.AddAsync(user);
                await _identityDbContext.SaveChangesAsync();

                return RegistrationStatus.Success;
            }
            catch (Exception)
            {

                return RegistrationStatus.Error;
            }
        }

    }
}
