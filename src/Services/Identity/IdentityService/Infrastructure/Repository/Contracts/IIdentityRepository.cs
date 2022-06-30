using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Contracts
{
    public interface IIdentityRepository
    {
        Task<RegistrationStatus> TryRegisterAsync(User user);

        Task<User> TryGetUserByLoginAndPasswordAsync(string login, string password);
    }

    public enum RegistrationStatus
    {
        Success = 1,
        AccountExists,
        Error
    }

}
