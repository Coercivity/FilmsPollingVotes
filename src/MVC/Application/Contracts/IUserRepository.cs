using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(Guid id);
    }
}
