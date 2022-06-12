using Polling.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace Polling.Application.Behaviour
{
    public class WeightCalculator : IWeightCalculator
    {

        public async Task<int> CalculateWeightByUserAndMeetingIdAsync(Guid userId, Guid meetingId)
        {
            throw new NotImplementedException();
        }
    }
}
