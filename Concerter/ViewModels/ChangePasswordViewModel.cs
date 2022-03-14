using System.Reactive;
using System.Text.RegularExpressions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ChangePasswordViewModel : ViewModelBase
{
    private const string PasswordPattern =
        @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*_~’”|?<>.,\-\\\/])([a-zA-Z0-9!@#$%^&*_~’”|?<>.,\-\\\/]{8,16})$";

    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public ChangePasswordViewModel()
    {
        var canExecute = this.WhenAnyValue(
            model => model.Password,
            password => !string.IsNullOrWhiteSpace(password) &&
                        Regex.IsMatch(password, PasswordPattern));

        ChangePassword = ReactiveCommand.Create(() =>
        {
            //
        }, canExecute);
    }

    [Reactive]
    public string Password { get; set; }

    public ReactiveCommand<Unit, Unit> ChangePassword { get; }
}