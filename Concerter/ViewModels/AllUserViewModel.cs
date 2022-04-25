using System.Collections.ObjectModel;
using Concerter.Models;

namespace Concerter.ViewModels;

public class AllUserViewModel : ViewModelBase
{
    public AllUserViewModel()
    {
        Users.Add(new User
        {
            Email = "timkin.moxim@mail.ru",
            FirstName = "Максим",
            LastName = "Тимкин",
            MiddleName = "Дмитриевич",
            Password = "P@ssw0rd",
            Role = new Role { Name = "Admin" }
        });
        Users.Add(new User
        {
            Email = "test@mail.ru",
            FirstName = "Test1",
            LastName = "Test2",
            Password = "P@ssw0rd"
        });
    }

    public ObservableCollection<User> Users { get; } = new();
}