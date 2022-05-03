using System;
using System.Collections.Generic;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventTicketsViewModel : ViewModelBase
{
    /// <summary>
    /// Для отображения в предпросмотре в View
    /// </summary>
    public EventTicketsViewModel()
    {
        EventList = new EventListViewModel(Event.GetEvents(), RoleAccess.Cashier);
    }

    public EventTicketsViewModel(IEnumerable<Event> events)
    {
        EventList = new EventListViewModel(events, RoleAccess.Cashier);
    }

    public EventTicketsViewModel(DateOnly date)
    {
        EventList = new EventListViewModel(Event.GetEvents(), date, RoleAccess.Cashier);
    }

    [Reactive]
    public EventListViewModel EventList { get; set; }
}