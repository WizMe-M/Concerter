using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Concerter.Models
{
    public partial class Type
    {
        public Type()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Event> Events { get; set; }

        public static IEnumerable<Type> GetTypes()
        {
            using var context = new EP_02_01Context();
            return context.Types.ToArray();
        }
    }
}
