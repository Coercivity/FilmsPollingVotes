using System;

namespace Domain.Entities
{
    public class PollingModel
    {
        public Guid MeetingId { get; init; }
        public Guid CreatorId { get; init; }
        public Guid EntityId { get; set; }
        public float CreatorWeight { get; set; }
    }
}

