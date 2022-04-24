﻿using System;
using System.Collections.Generic;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventTicketsViewModel : ViewModelBase
{
    public EventTicketsViewModel(IEnumerable<Event> events)
    {
        EventList = new EventListViewModel(events);
    }

    /// <summary>
    /// Для отображения в предпросмотре в View
    /// </summary>
    public EventTicketsViewModel()
    {
        EventList = new EventListViewModel(Event.GetEvents());
    }

    public EventTicketsViewModel(DateOnly date)
    {
        EventList = new EventListViewModel(Event.GetEvents(), date);
    }

    [Reactive]
    public EventListViewModel EventList { get; set; }
}