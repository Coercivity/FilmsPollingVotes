using System;
using System.Threading.Tasks;

namespace Polling.Application.Contracts
{
    public interface IEntityPositionPicker
    {
        Task<Guid> PickPositionAsync(Guid meetingId);
    }
}
