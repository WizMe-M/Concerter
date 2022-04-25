using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public virtual ICollection<Event> Events { get; set; }

        public static IEnumerable<CulturalBuilding> GetBuildings()
        {
            using var context =new EP_02_01Context();
            context.CulturalBuildings.Load();
            return context.CulturalBuildings.ToArray();
        }
    }
}
