using System.Reactive;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class MyProfileViewModel : ViewModelBase
{
    private readonly ObservableAsPropertyHelper<string> _editOrSaveText;
    private readonly User _userProfile;

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
        _userProfile = new User();
        Email = "test@mail.ru";
        LastName = "Иванов";
        FirstName = "Иван";
        MiddleName = "Иванович";
        Password = "P@ssw0rd";

        ChangePassword = ReactiveCommand.Create(() => 
            new ChangePasswordViewModel(_userProfile));
        SwitchEditMode = ReactiveCommand.CreateFromTask(async () =>
        {
            EditMode = !EditMode;
            if (!EditMode)
            {
                _userProfile.FirstName = FirstName;
                _userProfile.LastName = LastName;
                _userProfile.MiddleName = MiddleName;

                await _userProfile.SaveAsync();
            }
        });
        Back = ReactiveCommand.Create(() => _userProfile);
    }

    public MyProfileViewModel(User user) : this()
    {
        EditMode = false;

        _userProfile = user;
        Email = _userProfile.Email;
        LastName = _userProfile.LastName;
        FirstName = _userProfile.FirstName;
        MiddleName = _userProfile.MiddleName;
        Password = _userProfile.Password;
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