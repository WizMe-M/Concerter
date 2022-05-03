using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class MyProfileViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<string> _editOrSaveText;
    private readonly User _user = null!;
    
    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public MyProfileViewModel()
    {
        this.WhenAnyValue(model => model.EditMode,
                mode => mode ? "Сохранить изменения" : "Редактировать профиль")
            .ToProperty(this, nameof(EditOrSaveText), out _editOrSaveText);

        EditMode = true;
        EditMode = false;
        Email = "test@mail.ru";
        LastName = "Иванов";
        FirstName = "Иван";
        MiddleName = "Иванович";
        Password = "P@ssw0rd";

        ChangePassword = ReactiveCommand.Create(() => new ChangePasswordViewModel(_user));
        SwitchEditMode = ReactiveCommand.CreateFromTask(async () =>
        {
            EditMode = !EditMode;
            if (!EditMode)
            {
                _user.FirstName = FirstName;
                _user.LastName = LastName;
                _user.MiddleName = MiddleName;

                await _user.SaveAsync();
            }
        });
        Back = ReactiveCommand.Create(() => _user);
    }

    public MyProfileViewModel(User user) : this()
    {
        _user = user;
        EditMode = false;
        Email = user.Email;
        LastName = user.LastName;
        FirstName = user.FirstName;
        MiddleName = user.MiddleName;
        Password = user.Password;
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

    public ReactiveCommand<Unit, ChangePasswordViewModel> ChangePassword { get; }

    [Reactive]
    public bool EditMode { get; set; }

    public ReactiveCommand<Unit, Unit> SwitchEditMode { get; }

    public string EditOrSaveText => _editOrSaveText.Value;

    public ReactiveCommand<Unit, User> Back { get; }
}