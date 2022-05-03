using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ReturnTicketsViewModel : ViewModelBase
{
    private int _hiddenCount;
    private readonly int _id;

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
            selector: client => client is not null);

        Return = ReactiveCommand.Create(() =>
        {
            SelectedClient!.Return(TicketCount);
            SelectedClient = null!;
            FillComboBox();
        }, canSell);

        Back = ReactiveCommand.Create(() => { MainWindowViewModel.Instance.Content = new EventInfoViewModel(_id); });

        //init
        IsAllTickets = true;
        TicketCount = 1;
        AllTickets = 10;
        Clients = new ObservableCollection<Ticket>
        {
            new()
            {
                SecondName = "Иванов",
                FirstName = "Иван",
                Amount = 10,
                EventId = 1
            }
        };
    }

    public ReturnTicketsViewModel(int id) : this()
    {
        _id = id;
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

    private async void FillComboBox()
    {
        Clients.Clear();
        var clients = await Ticket.GetClients(_id);
        foreach (var client in clients)
        {
            Clients.Add(client);
        }
    }
}