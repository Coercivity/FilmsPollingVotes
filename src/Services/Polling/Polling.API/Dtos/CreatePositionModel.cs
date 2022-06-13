using System;
using System.ComponentModel.DataAnnotations;

namespace Polling.API.Dtos
{
    public record CreatePositionModel
    {
        [Required]
        public Guid MeetingId { get; init; }
        [Required]
        public Guid CreatorId { get; init; }
        [Required]
        public Guid EntityId { get; set; }
        [Required]
        public float CreatorWeight { get; set; }
    }
}
