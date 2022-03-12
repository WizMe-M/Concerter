using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class Category
    {
        public Category()
        {
            CulturalBuildings = new HashSet<CulturalBuilding>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CulturalBuilding> CulturalBuildings { get; set; }
    }
}
