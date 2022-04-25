using System.Collections.ObjectModel;
using Concerter.Models;

namespace Concerter.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    public RegisterViewModel()
    {
        Roles = new ObservableCollection<Role>(Role.GetRoles());
    }

    public ObservableCollection<Role> Roles { get; }
}