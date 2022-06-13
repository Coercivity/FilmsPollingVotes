using Polling.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace Polling.Application.Behaviour
{
    public class EntityPositionPicker : IEntityPositionPicker
    {
        public Task<Guid> PickPositionAsync(Guid meetingId)
        {
            throw new NotImplementedException();
        }
    }
}
