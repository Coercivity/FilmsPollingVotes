using System;

namespace Polling.API.Dtos
{
    public record PositionModel
    {
        public Guid Id { get; init; }
        public Guid MeetingId { get; init; }
        public Guid CreatorId { get; init; }
        public int Weight { get; set; }
    }
}
