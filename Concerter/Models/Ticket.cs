using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Concerter.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;

namespace Concerter.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string SecondName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public int Amount { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        public static async Task<IEnumerable<Ticket>> GetClients(Event e)
        {
            await using var context = new EP_02_01Context();
            var tickets = context.Tickets.Where(ticket => ticket.EventId == e.Id);
            return tickets.ToArray();
        }

        public void Return(int ticketCount)
        {
            using var context = new EP_02_01Context();

            var ticket = context.Tickets.FirstOrDefault(t => t.Id == Id);
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

        public static void Sell(string firstName, string lastName, int ticketCount)
        {
            using var context = new EP_02_01Context();

            var search = context.Tickets.FirstOrDefault(
                t => t.FirstName == firstName && t.SecondName == lastName);

            if (search is not null)
            {
                search.Amount += ticketCount;
                context.Tickets.Update(search);
            }
            else
            {
                var ticket = new Ticket
                {
                    FirstName = firstName,
                    SecondName = lastName,
                    Amount = ticketCount
                };

                context.Tickets.Add(ticket);
            }

            context.SaveChanges();
        }
    }
}