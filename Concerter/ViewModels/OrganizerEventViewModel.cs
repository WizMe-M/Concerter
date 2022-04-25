using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class OrganizerEventTicketsViewModel :ViewModelBase
{
    [Reactive]
    public EventListViewModel EventList { get; set; }

    public OrganizerEventTicketsViewModel()
    {
        EventList = new EventListViewModel(Event.GetEvents());
    }
}