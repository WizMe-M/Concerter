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
            Header = new HeaderViewModel();
            Header.LogOut
                .Execute()
                .Subscribe(viewmodel =>
                {
                    viewmodel.Authorize
                        .Subscribe(async model =>
                        {
                            Debug.WriteLine($"{model?.Id} - [{model?.Email} ~ {model?.Password}]");
                            await AuthorizeAsync(model);
                        });
                    Content = viewmodel;
                });
            Header.Profile
                .Subscribe(viewmodel => { Content = viewmodel; });
        }

        [Reactive]
        public HeaderViewModel Header { get; set; }

        [Reactive]
        public ViewModelBase Content { get; set; }

        private async Task AuthorizeAsync(User? user)
        {
            if (user is null)
            {
                // ShowError("Пользователь с такими данными отсутствует в базе данных");
                Debug.WriteLine("Пользователь с такими данными отсутствует в базе данных");
                return;
            }

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