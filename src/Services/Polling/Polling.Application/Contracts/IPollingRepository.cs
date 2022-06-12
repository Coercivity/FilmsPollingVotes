using Polling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polling.Application.Contracts
{
    public interface IPollingRepository
    {
        Task CreatePositionAsync(EntityPosition entity);
        Task<EntityPosition> GetPositionByIdAsync(Guid id);
        Task<IEnumerable<EntityPosition>> GetPositionsByMeetingIdAsync(Guid id);
        Task DeletePositionAsync(EntityPosition entityPosition);
       

    }
}
