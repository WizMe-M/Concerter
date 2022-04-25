using System.Collections.ObjectModel;
using Concerter.Models;

namespace Concerter.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    public UserProfileViewModel()
    {
        Roles = new ObservableCollection<Role>(Role.GetRoles());
    }

    public ObservableCollection<Role> Roles { get; }
}