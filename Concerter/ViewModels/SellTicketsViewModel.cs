using System;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class SellTicketsViewModel : ViewModelBase
{
    private readonly Event _event;

    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public SellTicketsViewModel()
    {
        var canSell = this.WhenAnyValue(
            model => model.FirstName,
            model => model.LastName,
            (firstName, lastName) =>
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(lastName));
        
        FirstName = "Иван";
        LastName = "Иванов";
        TicketCount = 1;
        
        Sell = ReactiveCommand.Create(() =>
        {
            Ticket.Sell(FirstName, LastName, TicketCount);
            FirstName = "";
            LastName = "";
            TicketCount = 1;
        }, canSell);

        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Window.Content = new EventInfoViewModel(_event);
        });
    }

    public SellTicketsViewModel(Event e) : this()
    {
        _event = e;
    }

    [Reactive]
    public string FirstName { get; set; }

    [Reactive]
    public string LastName { get; set; }

    [Reactive]
    public int TicketCount { get; set; }

    public ReactiveCommand<Unit, Unit> Sell { get; }

    public ReactiveCommand<Unit, Unit> Back { get; }
}