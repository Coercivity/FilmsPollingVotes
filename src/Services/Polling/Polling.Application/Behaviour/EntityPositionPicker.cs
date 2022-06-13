using Polling.Application.Contracts;
using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polling.Application.Behaviour
{
    public class EntityPositionPicker : IEntityPositionPicker
    {
        private readonly IPollingRepository _pollingRepository;


        public EntityPositionPicker(IPollingRepository pollingRepository)
        {
            _pollingRepository = pollingRepository;
        }

        public async Task<Guid> PickPositionAsync(Guid meetingId)
        {

            var positions = (List<EntityPosition>)await _pollingRepository
                .GetPositionsByMeetingIdAsync(meetingId);

            var sum = 0f;

            var sums = new List<float>();

            foreach (var position in positions)
            {
                sum += position.Weight;
                sums.Add(sum);
            }

            var index = BinarySearchPicker(sum, sums);


            return positions[index].EntityId;

        }


        private int BinarySearchPicker(float sum, List<float> sums)
        {
            Random random = new Random();
            var target = random.NextDouble() * sum;

            int low = 0;
            int high = sums.Count - 1;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (target > sums[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }
            return low;
        }


    }
}
