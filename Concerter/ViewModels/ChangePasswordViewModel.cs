using System.Reactive;
using System.Text.RegularExpressions;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    private const string PasswordPattern =
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_~’”|?<>.,\-\\\/])([a-zA-Z0-9!@#$%^&*_~’”|?<>.,\-\\\/]{8,16})$";

    private readonly User _user;

    public ChangePasswordViewModel()
    {
        _user = new User();
        Password = "";
        
        var canExecute = this.WhenAnyValue(
            model => model.Password,
            password => !string.IsNullOrWhiteSpace(password) &&
                        Regex.IsMatch(password, PasswordPattern));

        SaveChangedPassword = ReactiveCommand.CreateFromTask(async () =>
        {
            _user.Password = Password;
            await _user.SaveAsync();
            return _user;
        }, canExecute);
    }

    public ChangePasswordViewModel(User user) : this()
    {
        Password = user.Password;
        _user = user;
    }

    [Reactive]
    public string Password { get; set; }

    public ReactiveCommand<Unit, User> SaveChangedPassword { get; }
}