using Application.Contracts;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly LobbyDbContext _lobbyDbContext;

        public MeetingRepository(LobbyDbContext lobbyDbContext)
        {
            _lobbyDbContext = lobbyDbContext;
        }


        public async Task CreateMeetingAsync(Meeting meeting)
        {
            await _lobbyDbContext.AddAsync(meeting);
            await _lobbyDbContext.SaveChangesAsync();
        }

        public async Task<Meeting> GetMeetingAsync(Guid id)
        {
            var meeting = await _lobbyDbContext.Meetings.FindAsync(id);

            if (meeting is not null)
            {
                return meeting;
            }

            return null;
        }

        public async Task RemoveMeetingAsync(Guid id)
        {
            var meeting = await GetMeetingAsync(id);

            _lobbyDbContext.Remove(meeting);

            await _lobbyDbContext.SaveChangesAsync();

        }
    }
}
