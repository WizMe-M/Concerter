using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class MyProfileViewModel : ViewModelBase
{
    public readonly string[] _buttonTexts = new[]
    {
        "Редактировать профиль",
        "Сохранить изменения"
    };

    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public MyProfileViewModel()
    {
        EditOrSaveText = _buttonTexts[0];
        this.WhenAnyValue(model => model.EditMode,
                value => value ? _buttonTexts[1] : _buttonTexts[0])
            .ToProperty(this, EditOrSaveText);
        
        EditMode = false;

        Email = "test@mail.ru";
        LastName = "Иванов";
        FirstName = "Иван";
        MiddleName = "Иванович";
        Password = "P@ssw0rd";
        ChangePassword = ReactiveCommand.Create(() => { });
        SwitchEditMode = ReactiveCommand.Create(() => { EditMode = !EditMode; });
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

    [Reactive]
    public string EditOrSaveText { get; set; }
}