using System;
using System.Collections.Generic;

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
        public int ImpresarioId { get; set; }

        public virtual CulturalBuilding CulturalBuilding { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual User Impresario { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Type Type { get; set; } = null!;
        public virtual ICollection<ParticipatingArtist> ParticipatingArtists { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
