using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI;

namespace Concerter.ViewModels;

public class EventListViewModel : ViewModelBase
{
    private DateTime _selectedDate;

    public EventListViewModel(IEnumerable<Event> events)
    {
        foreach (var @event in events)
        {
            AllEvents.Add(new EventViewModel(@event));
        }
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => this.RaiseAndSetIfChanged(ref _selectedDate, value);
    }

    public IEnumerable<EventViewModel> SelectedDateEvents { get; set; }
}