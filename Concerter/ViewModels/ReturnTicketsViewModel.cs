using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ReturnTicketsViewModel : ViewModelBase
{
    private readonly Event _event;
    private int _hiddenCount;

    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public ReturnTicketsViewModel()
    {
        this.WhenAnyValue(model => model.SelectedClient)
            .Subscribe(ticket =>
            {
                if (ticket is null) return;

                _hiddenCount = 1;
                IsAllTickets = true;
                AllTickets = ticket.Amount;
                TicketCount = AllTickets;
            });

        this.WhenAnyValue(model => model.IsAllTickets)
            .Subscribe(flag =>
            {
                if (flag)
                {
                    _hiddenCount = TicketCount;
                    TicketCount = AllTickets;
                    return;
                }

                TicketCount = _hiddenCount;
            });


        var canSell = this.WhenAnyValue(
            model => model.SelectedClient,
            selector: ticket => ticket is not null);

        //TODO: написать код команд для кнопок
        Return = ReactiveCommand.Create(() =>
        {
            SelectedClient!.Return(TicketCount);
            SelectedClient = null!;
            FillComboBox();
        }, canSell);

        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Window.Content = new EventInfoViewModel(_event);
        });

        //init
        IsAllTickets = true;
        TicketCount = 1;
        AllTickets = 10;
        Clients = new ObservableCollection<Ticket>
        {
            new()
            {
                Id = 1,
                SecondName = "Иванов",
                FirstName = "Иван",
                Amount = 10
            }
        };
    }

    public ReturnTicketsViewModel(Event e) : this()
    {
        _event = e;
        FillComboBox();
    }

    public ObservableCollection<Ticket> Clients { get; }

    [Reactive]
    public Ticket SelectedClient { get; set; }

    [Reactive]
    public bool IsAllTickets { get; set; }

    [Reactive]
    public int TicketCount { get; set; }

    [Reactive]
    public int AllTickets { get; set; }

    public ReactiveCommand<Unit, Unit> Return { get; }

    public ReactiveCommand<Unit, Unit> Back { get; }

    private void FillComboBox()
    {
        Clients.Clear();
        var clients = Ticket.GetClients(_event);
        foreach (var client in clients)
        {
            Clients.Add(client);
        }
    }
}