using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polling.Application.Contracts
{
    public interface IPollingRepository
    {
        Task CreatePositionAsync(EntityPosition position);
        Task<EntityPosition> GetPositionByIdAsync(Guid id);
        Task<IEnumerable<EntityPosition>> GetPositionsByMeetingIdAsync(Guid id);
        Task DeletePositionAsync(EntityPosition position);
        Task<IEnumerable<EntityPosition>> GetPositionsByMeetingAndCreatorIdAsync(Guid userId, Guid meetingId);

        Task UpdateWeightsAsync(IEnumerable<EntityPosition> positions);



    }
}
