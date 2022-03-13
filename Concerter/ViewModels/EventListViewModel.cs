using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventListViewModel : ViewModelBase
{
    public EventListViewModel(IEnumerable<Event> events)
    {
        foreach (var @event in events)
        {
            AllEvents.Add(new EventViewModel(@event));
        }
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    public IEnumerable<EventViewModel> SelectedDateEvents { get; set; }

    [Reactive]
    public DateTime SelectedDate { get; set; }
}