using System;
using System.Threading.Tasks;

namespace Polling.Application.Contracts
{
    public interface IVoteWeightCalculator
    {
        Task CalculateWeightsByUserAndMeetingIdAsync(Guid userId, Guid meetingId, float creatorWheight);
    }
}
