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

        //TODO: сделать методы для билетов
        public static IEnumerable<Ticket> GetClients(Event e)
        {
            throw new NotImplementedException();
        }

        public void Return(int ticketCount)
        {
            throw new NotImplementedException();
        }

        public static void Sell(string firstName, string lastName, int ticketCount)
        {
            throw new NotImplementedException();
        }
    }
}
