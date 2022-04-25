using System.Collections.ObjectModel;
using System.Reactive;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class RewardArtistsViewModel : ViewModelBase
{
    public RewardArtistsViewModel()
    {
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = null });
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = "" });
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = "Андреевич" });

        PrizeFirst = 150;
        PrizeSecond = 150;
        PrizeThird = 150;
    }

    public ObservableCollection<User> Artists { get; } = new();

    [Reactive]
    public User ChosenFirst { get; set; }

    [Reactive]
    public User ChosenSecond { get; set; }

    [Reactive]
    public User ChosenThird { get; set; }

    [Reactive]
    public decimal PrizeFirst { get; set; }

    [Reactive]
    public decimal PrizeSecond { get; set; }

    [Reactive]
    public decimal PrizeThird { get; set; }
}