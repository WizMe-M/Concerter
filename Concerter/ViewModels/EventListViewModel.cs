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
    public IEnumerable<EventViewModel> SelectedDateEvents => _selectedDateEvents.Value;

    public EventListViewModel()
    {
        this.WhenAnyValue(
                model => model.SelectedDate,
                date =>
                {
                    return AllEvents
                        .Where(e => e.Date == date)
                        .OrderBy(model => model.Time);
                })
            .ToProperty(this, nameof(SelectedDateEvents), out _selectedDateEvents);

        this.WhenAnyValue(model => model.SelectedEvent)
            .Subscribe(viewModel =>
            {
                if (viewModel is null) return;
                
                MainWindowViewModel.Window.Content = new EventInfoViewModel(viewModel.Id);
            });

        SelectedDate = DateOnly.FromDateTime(DateTime.Today);

        Previous = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(-1); });
        Next = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(1); });
    }

    public EventListViewModel(IEnumerable<Event> events) : this()
    {
        foreach (var e in events)
            AllEvents.Add(new EventViewModel(e));
    }

    public EventListViewModel(IEnumerable<Event> events, DateOnly date)
    {
        foreach (var e in events)
            AllEvents.Add(new EventViewModel(e));
        SelectedDate = date;
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    [Reactive]
    public DateOnly SelectedDate { get; set; }

    [Reactive]
    public EventViewModel SelectedEvent { get; set; } = null!;

    public ReactiveCommand<Unit, Unit> Previous { get; }
    public ReactiveCommand<Unit, Unit> Next { get; }
}