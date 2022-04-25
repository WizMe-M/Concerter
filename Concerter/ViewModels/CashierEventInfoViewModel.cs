using System;
using System.Linq;
using System.Reactive;
using Concerter.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventInfoViewModel : ViewModelBase
{
    private readonly DateOnly _date;
    private readonly int _maxCapacity;
    private readonly int _id;

    public EventInfoViewModel()
    {
        var canSell = this.WhenAnyValue(
            model => model.CountLeftTickets,
            sold => sold > 0);

        var canReturn = this.WhenAnyValue(
            model => model.CountLeftTickets,
            sold => _maxCapacity - sold > 0);

        SellTickets = ReactiveCommand.Create(
            () => { MainWindowViewModel.Window.Content = new SellTicketsViewModel(_id, CountLeftTickets); },
            canSell);

        ReturnTickets = ReactiveCommand.Create(
            () => { MainWindowViewModel.Window.Content = new ReturnTicketsViewModel(_id); },
            canReturn);

        Back = ReactiveCommand.CreateFromTask(async () =>
        {
            var events = await Event.GetEventsAsync();
            MainWindowViewModel.Window.Content = new EventTicketsViewModel(_date);
        });

        //init
        Name = "День рождения Иоганна Себастьяна Баха";
        Price = 200;
        CulturalBinding = "Бар \"Грибная поляна\"";
        Address = "353051, Ульяновская область, город Кашира, ул. Ломоносова, 59";
        Description = 
            @"За последний год проект ЛСП успел неоднократно взлететь в топ-чарты, выпустить космические релизы и отыграть концерты по всей стране, включая два столичных солдаута в MMD.

Смертельно романтичный бэнд снова вывернет наизнанку твою душу на сцене Music Media Dome, продолжив традицию больших весенних концертов.";
        CountLeftTickets = -1;
    }

    public EventInfoViewModel(int id) : this()
    {
        using var context = new EP_02_01Context();
        var e = context.Events
            .Include(e => e.CulturalBuilding)
            .Include(e => e.Tickets)
            .FirstOrDefault(e => e.Id == id)!;

        _id = id;
        Name = e.Name;
        Description = e.Description;
        Price = e.Cost;
        CulturalBinding = e.CulturalBuilding.Name;
        Address = e.CulturalBuilding.Address;
        
        _maxCapacity = e.CulturalBuilding.Capacity;
        var sold = e.Tickets.Select(ticket => ticket.Amount).Sum();
        CountLeftTickets = _maxCapacity - sold;
        _date = e.Date;
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