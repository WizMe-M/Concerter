using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventListViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<IEnumerable<EventViewModel>> _selectedDateEvents;

    public EventListViewModel()
    {
        SelectedDate = DateTime.Today;
        this.WhenAnyValue(model => model.SelectedDate,
                date => AllEvents.Where(e => e.Date == DateOnly.FromDateTime(date)))
            .ToProperty(this, nameof(SelectedDateEvents), out _selectedDateEvents);

        Previous = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(-1); });
        Next = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(1); });
    }

    public EventListViewModel(IEnumerable<Event> events) : this()
    {
        foreach (var @event in events)
            AllEvents.Add(new EventViewModel(@event));
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    public IEnumerable<EventViewModel> SelectedDateEvents => _selectedDateEvents.Value;

    [Reactive]
    public DateTime SelectedDate { get; set; }

    public ReactiveCommand<Unit, Unit> Previous { get; }
    public ReactiveCommand<Unit, Unit> Next { get; }
}