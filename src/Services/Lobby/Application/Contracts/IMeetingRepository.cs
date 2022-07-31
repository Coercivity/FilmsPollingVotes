using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IMeetingRepository
    {
        Task CreateMeetingAsync(Meeting meeting);

        Task<Meeting> GetMeetingAsync(Guid id);

        Task RemoveMeetingAsync(Guid id);
    }
}
