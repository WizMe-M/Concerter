using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            using var context = new EP_02_01Context();
            context.CulturalBuildings.Load();
            return context.CulturalBuildings.ToArray();
        }

        public static bool Exists(string name)
        {
            using var context = new EP_02_01Context();
            return context.CulturalBuildings.FirstOrDefault(building => building.Name == name) is not null;
        }

        public void Add()
        {
            using var context = new EP_02_01Context();
            context.CulturalBuildings.Add(this);
            context.SaveChanges();
        }
        
        public async Task SaveAsync()
        {
            await using var context = new EP_02_01Context();
            var culturalBuilding = (await context.CulturalBuildings.FirstOrDefaultAsync(c => c.Id == Id))!;
            culturalBuilding.Name = Name;
            culturalBuilding.Address = Address;
            culturalBuilding.Capacity =Capacity;
            context.CulturalBuildings.Update(culturalBuilding);
            await context.SaveChangesAsync();
        }

        public static void Delete(int id)
        {
            using var context = new EP_02_01Context();
            var culturalBuilding = context.CulturalBuildings.FirstOrDefault(c => c.Id == id)!;
            context.CulturalBuildings.Remove(culturalBuilding);
            context.SaveChanges();
        }
    }
}