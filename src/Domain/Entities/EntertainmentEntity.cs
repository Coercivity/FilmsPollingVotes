



using System;

namespace Domain.Entities
{
    public abstract class EntertainmentEntity
    {
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int KinopoiskId { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }

}


