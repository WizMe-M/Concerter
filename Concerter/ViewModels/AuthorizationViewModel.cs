using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AuthorizationViewModel : ViewModelBase
{
    public AuthorizationViewModel()
    {
        var canExecute = this.WhenAnyValue(
            viewModel => viewModel.Email,
            viewModel => viewModel.Password,
            (email, password) =>
                !string.IsNullOrWhiteSpace(email)
                && !string.IsNullOrWhiteSpace(password));

        // Authorize = ReactiveCommand.CreateFromTask(
        //     async () =>
        //     {
        //         AuthorizedUser = await User.SearchAsync(Email!, Password!);
        //     },
        //     canExecute);
        Authorize = ReactiveCommand.Create(() =>
        {
            var user = User.Search(Email!, Password!);
            Debug.WriteLine($"Result search: user id is {user?.Id}");
            return user;
            // return new User()
            // {
            //     Id = 42,
            //     Email = Email!,
            //     Password = Password!
            // };
        }, canExecute);
    }

    [Reactive]
    public string Email { get; set; }

    [Reactive]
    public string Password { get; set; }

    public ReactiveCommand<Unit, User?> Authorize { get; }
}