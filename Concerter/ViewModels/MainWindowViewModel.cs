using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var viewmodel = new AuthorizationViewModel();
            // viewmodel.WhenAnyValue(model => model.AuthorizedUser)
            //     .Subscribe(x => Debug.WriteLine(x?.Id ?? -1));
            viewmodel.Authorize
                // .Take(1)
                .Subscribe(async model =>
                {
                    Debug.WriteLine($"{model?.Id} - [{model?.Email} ~ {model?.Password}]");
                    await AuthorizeAsync(model);
                });
            Content = viewmodel;
        }

        [Reactive]
        public ViewModelBase Content { get; set; }

        private void Authorize(User? user)
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
                    var enumerable = Event.GetEvents();
                    Content = new EventTicketsViewModel(enumerable);
                    break;
                default: throw new NotImplementedException();
            }
        }

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