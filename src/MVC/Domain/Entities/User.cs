using System;

namespace Domain.Entities
{
    public class User
    {
        public string Name { get; set; }
        public float CreatorWeight { get; set; }
        public Guid Id { get; set; }
    }
}
