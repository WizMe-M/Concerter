using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ArtistMyEventsViewModel : ViewModelBase
{
    [Reactive]
    public EventListViewModel EventList { get; set; }

    public ArtistMyEventsViewModel()
    {
        EventList = new EventListViewModel(Event.GetEvents());
    }
}