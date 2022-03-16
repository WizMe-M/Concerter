using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventInfoViewModel : ViewModelBase
{
    public EventInfoViewModel()
    {
        Name = "День рождения Иоганна Себстьяна Баха";
        Price = 200;
        CountLeftTickets = 49;
        CulturalBinding = "Бар \"Грибная поляна\"";
        Address = "353051, Ульяновская область, город Кашира, ул. Ломоносова, 59";
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
    
    public ReactiveCommand<Unit, Event> SellTickets { get; }
    public ReactiveCommand<Unit, Event> ReturnTickets { get; }
    public ReactiveCommand<Unit, Event> Back { get; }
}