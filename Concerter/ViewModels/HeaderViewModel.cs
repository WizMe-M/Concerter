using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class HeaderViewModel : ViewModelBase
{
    public HeaderViewModel()
    {
        LogOut = ReactiveCommand.Create(() =>
        {
            AuthorizedUser = null;
            return new AuthorizationViewModel();
        });

        Profile = ReactiveCommand.Create(() => 
            new MyProfileViewModel(AuthorizedUser!));
    }

    [Reactive]
    public User? AuthorizedUser { get; set; }

    public ReactiveCommand<Unit, AuthorizationViewModel> LogOut { get; }

    public ReactiveCommand<Unit, MyProfileViewModel> Profile { get; }
}