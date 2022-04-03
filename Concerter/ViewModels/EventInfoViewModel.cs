using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventInfoViewModel : ViewModelBase
{
    private readonly Event _event;

    public EventInfoViewModel()
    {
        var canSell = this.WhenAnyValue(model => model.CountLeftTickets,
            tickets => tickets > 0);
        
        SellTickets = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Window.Content = new SellTicketsViewModel(_event);
        }, canSell);
        
        ReturnTickets = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Window.Content = new ReturnTicketsViewModel(_event);
        });
        
        Back = ReactiveCommand.CreateFromTask(async () =>
        {
            var events = await Event.GetEventsAsync();
            MainWindowViewModel.Window.Content = new EventTicketsViewModel();
        });
        
        //init
        Name = "День рождения Иоганна Себастьяна Баха";
        Price = 200;
        CountLeftTickets = 49;
        CulturalBinding = "Бар \"Грибная поляна\"";
        Address = "353051, Ульяновская область, город Кашира, ул. Ломоносова, 59";
    }

    public EventInfoViewModel(Event e) : this()
    {
        _event = e;
    }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public decimal Price { get; set; }

    [Reactive]
    public int CountLeftTickets { get; set; }

    [Reactive]
    public string CulturalBinding { get; set; }

    [Reactive]
    public string Address { get; set; }

    public ReactiveCommand<Unit, Unit> SellTickets { get; }
    public ReactiveCommand<Unit, Unit> ReturnTickets { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }
}