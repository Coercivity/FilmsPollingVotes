using System;

namespace Polling.Domain.Entities
{
    public record EntityPosition
    {
        public Guid Id { get; init; }
        public Guid MeetingId { get; init; }
        public Guid CreatorId { get; init; }
        public int Weight { get; set; }

    }
}


