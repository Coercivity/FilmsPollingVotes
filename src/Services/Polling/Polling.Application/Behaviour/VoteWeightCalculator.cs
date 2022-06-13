using Polling.Application.Contracts;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polling.Application.Behaviour
{
    public class VoteWeightCalculator : IVoteWeightCalculator
    {
        private readonly IPollingRepository _pollingRepository;



        public VoteWeightCalculator(IPollingRepository pollingRepository)
        {
            _pollingRepository = pollingRepository;
        }


        public async Task CalculateWeightsByUserAndMeetingIdAsync(Guid userId, Guid meetingId, float creatorWeight)
        {
            var positions = (List<EntityPosition>)await _pollingRepository
                                    .GetPositionsByMeetingAndCreatorIdAsync(userId, meetingId);

            if (positions is not null)
            {
                await UpdateWeightsAsync(positions, creatorWeight);
            }
        }



        public async Task UpdateWeightsAsync(List<EntityPosition> positions, float creatorWeight)
        {

            var totalPositions = positions.Count;

            foreach (var position in positions)
            {
                position.CreatorWeight = creatorWeight;
                position.Weight = creatorWeight / totalPositions;
            }

            await _pollingRepository.UpdateWeightsAsync(positions);
        }

        
    }
}
