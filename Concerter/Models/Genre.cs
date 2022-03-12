using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }
    }
}
