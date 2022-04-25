namespace Concerter.ViewModels;

public class ImpresarioMyEventsViewModel : ViewModelBase
{
    public ImpresarioMyEventsViewModel()
    {
        EventList = new EventListViewModel();
    }

    public EventListViewModel EventList { get; set; }
}