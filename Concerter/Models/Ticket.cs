using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public int Amount { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
