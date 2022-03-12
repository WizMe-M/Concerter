using System;
using System.Collections.Generic;

namespace Concerter.Models
{
    public partial class Giving
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public decimal Prize { get; set; }
    }
}
