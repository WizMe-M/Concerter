using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class UserProfileViewModel : ViewModelBase
{
    private readonly User _user;
    public UserProfileViewModel()
    {
        _user = null!;
        Roles = new ObservableCollection<Role>(Role.GetRoles());
        
        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new AllUserViewModel();
        });
        
        var canToggle = this.WhenAnyValue(
            model => model.Email,
            model => model.LastName,
            model => model.FirstName,
            model => model.Password,
            model => model.SelectedRole,
            (email, lastName, firstName, password, role) => 
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(lastName) &&
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(password) &&
                role is not null || !IsEditMode);
        
        ChangeOrSave = ReactiveCommand.CreateFromTask(async () =>
        {
            IsEditMode = !IsEditMode;
            if (IsEditMode == false)
            {
                _user.Email = Email!;
                _user.LastName = LastName!;
                _user.FirstName = FirstName!;
                _user.MiddleName = MiddleName;
                _user.Password = Password!;
                _user.RoleId = SelectedRole!.Id;

                await _user.SaveAsync();
            }
        }, canToggle);
    }

    public UserProfileViewModel(User user) : this()
    {
        _user = user;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        MiddleName = user.MiddleName;
        Password = user.Password;
        SelectedRole = user.Role!;
    }

    [Reactive]
    public bool IsEditMode { get; set; }

    public ICommand ChangeOrSave { get; }

    public ICommand Back { get; }

    [Reactive]
    public string Email { get; set; }

    [Reactive]
    public string FirstName { get; set; }

    [Reactive]
    public string LastName { get; set; }

    [Reactive]
    public string? MiddleName { get; set; }

    [Reactive]
    public string Password { get; set; }

    [Reactive]
    public Role SelectedRole { get; set; }

    public ObservableCollection<Role> Roles { get; }
}