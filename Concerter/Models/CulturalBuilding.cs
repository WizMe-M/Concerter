using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class CulturalBuilding
    {
        public CulturalBuilding()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Capacity { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Event> Events { get; set; }
    }
}
