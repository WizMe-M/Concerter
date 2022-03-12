using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class ParticipatingArtist
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int EventId { get; set; }

        public virtual User Artist { get; set; } = null!;
        public virtual Event Event { get; set; } = null!;
    }
}
