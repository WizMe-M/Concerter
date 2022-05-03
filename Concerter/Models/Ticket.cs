using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Concerter.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Microsoft.EntityFrameworkCore;

namespace Concerter.Models
{
    public class Ticket
    {
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public int Amount { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        public static async Task<IEnumerable<Ticket>> GetClients(int id)
        {
            await using var context = new EP_02_01Context();
            var tickets = context.Tickets
                .Where(ticket => ticket.EventId == id)
                .OrderBy(ticket => ticket.SecondName );
            return tickets.ToArray();
        }

        public void Return(int ticketCount)
        {
            using var context = new EP_02_01Context();

            var ticket = context.Tickets.FirstOrDefault(
                t => t.FirstName == FirstName
                     && t.SecondName == SecondName
                     && t.EventId == EventId);
            if (ticket is null)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка!",
                    "Произошла ошибка, в базе данных нет записей о билетах данного посетителя",
                    ButtonEnum.Ok, Icon.Error).ShowDialog(MainWindow.Window);
                return;
            }

            if (ticket.Amount < ticketCount)
            {
                ticket.Amount -= ticketCount;
                context.Tickets.Update(ticket);
            }
            else if (ticket.Amount == ticketCount)
            {
                context.Tickets.Remove(ticket);
            }

            context.SaveChanges();
        }

        public static void Sell(int id, string firstName, string lastName, int ticketCount)
        {
            using var context = new EP_02_01Context();

            var search = context.Tickets.FirstOrDefault(
                t => t.FirstName == firstName
                     && t.SecondName == lastName
                     && t.EventId == id);

            if (search is not null)
            {
                search.Amount += ticketCount;
                context.Tickets.Update(search);
            }
            else
            {
                var ticket = new Ticket
                {
                    EventId = id,
                    FirstName = firstName,
                    SecondName = lastName,
                    Amount = ticketCount
                };

                context.Tickets.Add(ticket);
            }

            context.SaveChanges();
        }

        public static IEnumerable<Ticket> Select(DateOnly start, DateOnly end)
        {
            using var context = new EP_02_01Context();
            var tickets = context.Tickets
                .Include(ticket => ticket.Event)
                .Where(ticket => ticket.Event.Date >= start && ticket.Event.Date <= end)
                .ToArray();
            return tickets;
        }
    }
}