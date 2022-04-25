using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AssignImpresarioViewModel : ViewModelBase
{
    public AssignImpresarioViewModel()
    {
        Name = "День рождения Иоганна Себастьяна Баха";

        Impresarios.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = null });
        Impresarios.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = "" });
        Impresarios.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = "Андреевич" });
    }

    public ObservableCollection<User> Impresarios { get; } = new();

    [Reactive]
    public string Name { get; set; }
}