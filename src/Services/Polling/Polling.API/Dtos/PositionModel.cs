using System;

namespace Polling.API.Dtos
{
    public record PositionModel
    {
        public Guid Id { get; init; }
        public Guid EntityId { get; set; }
        public Guid MeetingId { get; init; }
        public Guid CreatorId { get; init; }

        public float Weight { get; set; }
        public float CreatorWeight { get; set; }
    }
}