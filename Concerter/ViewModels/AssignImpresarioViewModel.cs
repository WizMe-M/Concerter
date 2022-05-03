using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AssignImpresarioViewModel : ViewModelBase
{
    private readonly EventViewModel _event = null!;
    
    public AssignImpresarioViewModel()
    {
        Impresarios = new ObservableCollection<User>(User.GetImpresario());
        var canAssign = this.WhenAnyValue(model => model.Selected,
            selector: user => user is not null);
        
        AssignImpresario = ReactiveCommand.Create(() =>
        {
            Event.AssignImpresario(_event.Id, Selected!.Id);
            MainWindowViewModel.Instance.Content = new OrganizerEventInfoViewModel(_event);
        }, canAssign);

        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerEventInfoViewModel(_event);
        });
    }

    public AssignImpresarioViewModel(EventViewModel e) : this()
    {
        _event = e;
        Name = e.Name;
    }

    public ObservableCollection<User> Impresarios { get; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public User Selected { get; set; }
    
    public ICommand AssignImpresario { get; }
    public ICommand Back { get; }
}