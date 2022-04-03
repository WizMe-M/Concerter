using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Window = this;
            Header = new HeaderViewModel();
            Header.LogOut
                .Subscribe(ShowAuthorization);

            Header.Profile
                .Subscribe(ShowMyProfile);

            ShowAuthorization(new AuthorizationViewModel());
        }

        public static MainWindowViewModel Window { get; private set; }

        [Reactive]
        public HeaderViewModel Header { get; set; }

        [Reactive]
        public ViewModelBase Content { get; set; }

        private void ShowAuthorization(AuthorizationViewModel viewmodel)
        {
            viewmodel.Authorize
                .Subscribe(async model =>
                {
                    Debug.WriteLine($"{model?.Id} - [{model?.Email} ~ {model?.Password}]");
                    await AuthorizeAsync(model);
                });
            Content = viewmodel;
        }

        private void ShowMyProfile(MyProfileViewModel viewModel)
        {
            viewModel.ChangePassword
                .Subscribe(ShowChangePassword);
            viewModel.Back
                .Subscribe(async user => await AuthorizeAsync(user));
            Content = viewModel;
        }

        private void ShowChangePassword(ChangePasswordViewModel viewModel)
        {
            viewModel.SaveChangedPassword
                .Subscribe(async user => await AuthorizeAsync(user));
            Content = viewModel;
        }

        private async Task AuthorizeAsync(User? user)
        {
            if (user is null)
            {
                // ShowError("Пользователь с такими данными отсутствует в базе данных");
                Debug.WriteLine("Пользователь с такими данными отсутствует в базе данных");
                return;
            }

            Header.AuthorizedUser = user;
            Debug.WriteLine("Пользователь авторизован");
            var role = Role.AuthorizeUser(user.RoleId);
            Debug.WriteLine($"Роль пользователя: {role}");
            switch (role)
            {
                case RoleAccess.Cashier:
                    var enumerable = await Event.GetEventsAsync();
                    Content = new EventTicketsViewModel(enumerable);
                    break;
                default: throw new NotImplementedException();
            }
        }
    }
}