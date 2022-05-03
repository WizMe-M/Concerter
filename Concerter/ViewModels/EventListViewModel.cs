using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Concerter.Models;
using Concerter.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventListViewModel : ViewModelBase
{
    private bool isReadOnly = false;
    
    private readonly ObservableAsPropertyHelper<IEnumerable<EventViewModel>> _selectedDateEvents;
    public IEnumerable<EventViewModel> SelectedDateEvents => _selectedDateEvents.Value;

    public EventListViewModel()
    {
        this.WhenAnyValue(model => model.SelectedDate,
                date =>
                {
                    var events = AllEvents.Where(e => e.Date == date);
                    var ordered = events.OrderBy(e => e.Time);
                    return ordered;
                })
            .ToProperty(this, nameof(SelectedDateEvents), out _selectedDateEvents);

        SelectedDate = DateOnly.FromDateTime(DateTime.Today);

        Previous = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(-1); });
        Next = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(1); });
    }

    public EventListViewModel(IEnumerable<Event> events) : this()
    {
        foreach (var e in events)
            AllEvents.Add(new EventViewModel(e));
    }

    public EventListViewModel(IEnumerable<Event> events, DateOnly date) : this()
    {
        foreach (var e in events)
            AllEvents.Add(new EventViewModel(e));
        SelectedDate = date;
    }

    public EventListViewModel(IEnumerable<Event> events, RoleAccess role) : this(events)
    {
        CreateItemClickCommand(role);
        this.WhenAnyValue(model => model.SelectedEvent).InvokeCommand(ItemClick);
    }

    public EventListViewModel(IEnumerable<Event> events, DateOnly date, RoleAccess role) : this(events, date)
    {
        CreateItemClickCommand(role);
        this.WhenAnyValue(model => model.SelectedEvent).InvokeCommand(ItemClick);
    }

    private void CreateItemClickCommand(RoleAccess role)
    {
        ItemClick = role switch
        {
            RoleAccess.Cashier => ReactiveCommand.Create<EventViewModel, Unit>(model =>
            {
                if (model is null) return default;
                MainWindowViewModel.Instance.Content = new EventInfoViewModel(model.Id);
                return default;
            }),
            RoleAccess.Organizer => ReactiveCommand.Create<EventViewModel, Unit>(model =>
            {
                if (isReadOnly || model is null) return default;
                MainWindowViewModel.Instance.Content = new OrganizerEventInfoViewModel(model);
                return default;
            })
        };
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    [Reactive]
    public DateOnly SelectedDate { get; set; }

    [Reactive]
    public EventViewModel SelectedEvent { get; set; }

    public ReactiveCommand<Unit, Unit> Previous { get; }
    public ReactiveCommand<Unit, Unit> Next { get; }
    public ICommand ItemClick { get; set; }


    public void Select(EventViewModel found)
    {
        isReadOnly = true;
        SelectedEvent = found;
        isReadOnly = false;
    }
}