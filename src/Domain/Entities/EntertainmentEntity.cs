



using System;

namespace Domain.Entities
{
    public abstract class EntertainmentEntity
    {
        public Guid Id { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Link { get; set; }
        public float Weight { get; set; }

    }

}


