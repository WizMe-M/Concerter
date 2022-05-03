using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AllUserViewModel : ViewModelBase
{
    public AllUserViewModel()
    {
        Users = new ObservableCollection<User>(User.GetUsers(MainWindowViewModel.Instance.Header.AuthorizedUser!));
        
        this.WhenAnyValue(model => model.SelectedUser)
            .Subscribe(OpenProfile);
        
        Menu = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel();
        });
        
        Register = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new RegisterViewModel();
        });
    }

    public ObservableCollection<User> Users { get; }
    
    [Reactive]
    public User SelectedUser { get; set; }
    
    public ICommand Menu { get; }
    public ICommand Register { get; }

    private static void OpenProfile(User user)
    {
        if (user is null) return;

        MainWindowViewModel.Instance.Content = new UserProfileViewModel(user);
    }
}