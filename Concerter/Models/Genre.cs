using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public static IEnumerable<Genre> GetBuildings()
        {
            using var context = new EP_02_01Context();
            context.Genres.Load();
            return context.Genres.ToArray();
        }
    }
}