using System;

namespace Domain.Entities
{
    public class Film : EntertainmentEntity
    {
        public int KinopoiskId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string PosterUrl { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public float RatingKinopoisk { get; set; }
    }

}


