using System.Collections.Generic;
using Concerter.Models;

namespace Concerter.ViewModels;

public class EventTicketsViewModel : ViewModelBase
{
    public EventTicketsViewModel(IEnumerable<Event> events)
    {
        Events = new EventListViewModel(events);
    }

    public EventListViewModel Events { get; set; }
}