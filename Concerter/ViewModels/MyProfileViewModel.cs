using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class MyProfileViewModel : ViewModelBase
{
    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public MyProfileViewModel()
    {
        Email = "test@mail.ru";
        LastName = "Иванов";
        FirstName = "Иван";
        MiddleName = "Иванович";
        Password = "P@ssw0rd";
        ChangePassword = ReactiveCommand.Create(() => { });
        EditMode = false;
        SwitchEditMode = ReactiveCommand.Create(() => { });
    }

    public MyProfileViewModel(User user)
    {
        throw new System.NotImplementedException();
    }

    [Reactive]
    public string Email { get; set; }

    [Reactive]
    public string LastName { get; set; }

    [Reactive]
    public string FirstName { get; set; }

    [Reactive]
    public string? MiddleName { get; set; }

    [Reactive]
    public string Password { get; set; }

    public ReactiveCommand<Unit, Unit> ChangePassword { get; }

    [Reactive]
    public bool EditMode { get; set; }

    public ReactiveCommand<Unit, Unit> SwitchEditMode { get; }
}