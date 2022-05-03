using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Concerter.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    public RegisterViewModel()
    {
        var canRegister = this.WhenAnyValue(
            model => model.Email,
            model => model.FirstName,
            model => model.LastName,
            model => model.Password,
            model => model.SelectedRole,
            (email, firstName, lastName, password, role) =>
                !string.IsNullOrWhiteSpace(email) &&
                !string.IsNullOrWhiteSpace(firstName) &&
                !string.IsNullOrWhiteSpace(lastName) &&
                !string.IsNullOrWhiteSpace(password) &&
                role is not null);
        Roles = new ObservableCollection<Role>(Role.GetRoles());
        Register = ReactiveCommand.Create(() =>
        {
            if (!MailAddress.TryCreate(Email!, out _))
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Некорректный `Email`", ButtonEnum.Ok, Icon.Error).Show();
                return;
            }

            var user = new User
            {
                Email = Email!.Trim(),
                LastName = LastName!.Trim(),
                FirstName = FirstName!.Trim(),
                MiddleName = MiddleName?.Trim(),
                Password = Password!.Trim(),
                RoleId = SelectedRole!.Id
            };
            user.Register();
            MainWindowViewModel.Instance.Content = new AllUserViewModel();
        }, canRegister);
        Back = ReactiveCommand.Create(() => { MainWindowViewModel.Instance.Content = new AllUserViewModel(); });
    }

    [Reactive]
    public string Email { get; set; }

    [Reactive]
    public string FirstName { get; set; }

    [Reactive]
    public string LastName { get; set; }

    [Reactive]
    public string MiddleName { get; set; }

    [Reactive]
    public string Password { get; set; }

    [Reactive]
    public Role SelectedRole { get; set; }

    public ObservableCollection<Role> Roles { get; }
    public ICommand Back { get; }
    public ICommand Register { get; }
}