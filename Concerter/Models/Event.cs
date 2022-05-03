using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Concerter.Models
{
    public partial class Event
    {
        public Event()
        {
            ParticipatingArtists = new HashSet<ParticipatingArtist>();
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Cost { get; set; }
        public int CulturalBuildingId { get; set; }
        public int TypeId { get; set; }
        public int GenreId { get; set; }
        public int StatusId { get; set; }
        public int? ImpresarioId { get; set; }

        public virtual CulturalBuilding CulturalBuilding { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual User Impresario { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Type Type { get; set; } = null!;
        public virtual ICollection<ParticipatingArtist> ParticipatingArtists { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public static async Task<IEnumerable<Event>> GetEventsAsync()
        {
            await using var context = new EP_02_01Context();
            await context.Events
                .Include(@event => @event.Genre)
                .Include(@event => @event.CulturalBuilding)
                .LoadAsync();
            return await context.Events.ToArrayAsync();
        }

        public static IEnumerable<Event> GetEvents()
        {
            using var context = new EP_02_01Context();
            context.Events
                .Include(e => e.Genre)
                .Include(e => e.CulturalBuilding)
                .Load();
            return context.Events.ToArray();
        }

        public async Task SaveAsync()
        {
            await using var context = new EP_02_01Context();
            context.Events.Update(this);
            await context.SaveChangesAsync();
        }

        public async Task Delete()
        {
            await using var context = new EP_02_01Context();
            context.Events.Remove(this);
            await context.SaveChangesAsync();
        }

        public static Event Find(int id)
        {
            using var context = new EP_02_01Context();
            return context.Events
                .Include(e => e.Genre)
                .Include(e => e.CulturalBuilding)
                .Include(e => e.Type)
                .Include(e => e.Status)
                .FirstOrDefault(e => e.Id == id)!;
        }

        public void Add()
        {
            using var context = new EP_02_01Context();
            context.Events.Add(this);
            context.SaveChanges();
        }
    }
}