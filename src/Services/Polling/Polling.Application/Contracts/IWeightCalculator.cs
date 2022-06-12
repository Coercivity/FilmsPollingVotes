using System;
using System.Threading.Tasks;

namespace Polling.Application.Contracts
{
    public interface IWeightCalculator
    {
        Task<int> CalculateWeightByUserAndMeetingIdAsync(Guid userId, Guid meetingId);
    }
}
