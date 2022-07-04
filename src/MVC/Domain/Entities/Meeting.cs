using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Meeting
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string URL { get; set; }
        public List<User> Users { get; set; }
    }
}
