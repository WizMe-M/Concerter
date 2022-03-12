using System;
using System.Diagnostics;
using System.Reactive.Linq;
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
                .Take(1)
                .Subscribe(model =>
            {
                Console.WriteLine($"{model.Id} - [{model.Email} ~ {model.Password}]");
                Debug.WriteLine($"{model.Id} - [{model.Email} ~ {model.Password}]");
            });
            Content = viewmodel;
        }

        [Reactive]
        public ViewModelBase Content { get; set; }

        public void Authorize()
        {
            var vm = (AuthorizationViewModel)Content;
            vm.Authorize.Execute();
        }
        
        // private void Authorize(User? user)
        // {
        //     if (user is null)
        //     {
        //         // ShowError("Пользователь с такими данными отсутствует в базе данных");
        //         Debug.WriteLine("Пользователь с такими данными отсутствует в базе данных");
        //         return;
        //     }
        //     
        //     Debug.WriteLine("Пользователь авторизован");
        //     var role = AccountingService.AuthorizeUser(user.RoleId);
        //     Debug.WriteLine($"Роль пользователя: {role}");
        //     var service = new EventManagerService();
        //     switch (role)
        //     {
        //         case RoleAccess.Cashier:
        //             var task = service.GetEventsAsync();
        //             task.Start();
        //             var enumerable = task.Result;
        //             Content = new EventTicketsViewModel(enumerable);
        //             break;
        //         default: throw new NotImplementedException();
        //     }
        // }
        //
        // private async void AuthorizeAsync(User? user)
        // {
        //     if (user is null)
        //     {
        //         // ShowError("Пользователь с такими данными отсутствует в базе данных");
        //         Debug.WriteLine("Пользователь с такими данными отсутствует в базе данных");
        //         return;
        //     }
        //
        //     Debug.WriteLine("Пользователь авторизован");
        //     var role = AccountingService.AuthorizeUser(user.RoleId);
        //     Debug.WriteLine($"Роль пользователя: {role}");
        //     var service = new EventManagerService();
        //     switch (role)
        //     {
        //         case RoleAccess.Cashier:
        //             var enumerable = await service.GetEventsAsync();
        //             Content = new EventTicketsViewModel(enumerable);
        //             break;
        //         default: throw new NotImplementedException();
        //     }
        // }
    }
}