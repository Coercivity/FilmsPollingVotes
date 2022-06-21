using System;

namespace Domain.Entities
{
    public class Film : EntertainmentEntity
    {
        public int KinopoiskId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public string ImagePath { get; set; }
        public float RatingKinopoisk { get; set; }
    }

}


