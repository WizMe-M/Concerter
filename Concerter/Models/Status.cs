using System;
using System.Collections.Generic;
using System.Linq;

namespace Concerter.Models
{
    public partial class Status
    {
        public Status()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }

        public static IEnumerable<Status> GetStatuses()
        {
            using var context = new EP_02_01Context();
            return context.Statuses.ToArray();
        }
    }
}