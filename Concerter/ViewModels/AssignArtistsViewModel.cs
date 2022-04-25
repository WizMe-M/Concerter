using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AppointArtistsViewModel : ViewModelBase
{
    public AppointArtistsViewModel()
    {
        Name = "День рождения Иоганна Себастьяна Баха";

        NotAppointedArtists.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = null });
        NotAppointedArtists.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = "" });
        NotAppointedArtists.Add(new User { FirstName = "Андрей", LastName = "Андреев", MiddleName = "Андреевич" });

        AppointedArtists.Add(new User { FirstName = "Иван", LastName = "Иванов", MiddleName = null });
        AppointedArtists.Add(new User { FirstName = "Иван", LastName = "Иванов", MiddleName = "" });
        AppointedArtists.Add(new User { FirstName = "Иван", LastName = "Иванов", MiddleName = "Иванович" });
    }

    public ObservableCollection<User> NotAppointedArtists { get; } = new();
    public ObservableCollection<User> AppointedArtists { get; } = new();

    [Reactive]
    public string Name { get; set; }
}