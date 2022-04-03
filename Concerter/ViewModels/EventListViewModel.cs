using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Concerter.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventListViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<IEnumerable<EventViewModel>> _selectedDateEvents;

    public EventListViewModel()
    {
        this.WhenAnyValue(
                model => model.SelectedDate,
                date => AllEvents.Where(e => e.Date == DateOnly.FromDateTime(date)))
            .ToProperty(this, nameof(SelectedDateEvents), out _selectedDateEvents);

        this.WhenAnyValue(model => model.SelectedEvent)
            .Subscribe(async viewModel =>
            {
                if(viewModel is null) return;
                
                await using var database = new EP_02_01Context();
                var e = await database.Events.FirstOrDefaultAsync(e => e.Id == viewModel.Id);
                MainWindowViewModel.Window.Content = new EventInfoViewModel(e!);
            });
        
        SelectedDate = DateTime.Today;

        Previous = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(-1); });
        Next = ReactiveCommand.Create(() => { SelectedDate = SelectedDate.AddDays(1); });
    }

    public EventListViewModel(IEnumerable<Event> events) : this()
    {
        foreach (var e in events)
            AllEvents.Add(new EventViewModel(e));
    }

    public ObservableCollection<EventViewModel> AllEvents { get; } = new();

    public IEnumerable<EventViewModel> SelectedDateEvents => _selectedDateEvents.Value;

    [Reactive]
    public DateTime SelectedDate { get; set; }
    
    [Reactive]
    public EventViewModel SelectedEvent { get; set; }

    public ReactiveCommand<Unit, Unit> Previous { get; }
    
    public ReactiveCommand<Unit, Unit> Next { get; }
}