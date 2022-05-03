using System;
using System.Linq;
using System.Windows.Input;
using Concerter.Export;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ExportSellDataViewModel : ViewModelBase
{
    public ExportSellDataViewModel()
    {
        DateStart = DateTimeOffset.Now;
        DateEnd = DateTimeOffset.Now;
        
        Menu = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel();
        });

        Export = ReactiveCommand.Create(() =>
        {
            ExportSells();
            MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel();
        });
    }
    
    [Reactive]
    public DateTimeOffset DateStart { get; set; }
    
    [Reactive]
    public DateTimeOffset DateEnd { get; set; }

    public ICommand Menu { get; }
    public ICommand Export { get; }

    private void ExportSells()
    {
        var tickets = Ticket.Select(DateOnly.FromDateTime(DateStart.DateTime), DateOnly.FromDateTime(DateEnd.DateTime));
        var excel = new ExcelExporter();
        excel.Export(tickets);
    }
}