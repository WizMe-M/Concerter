using System.Collections.ObjectModel;
using System.Reactive;
using Concerter.Models;
using ReactiveUI;

namespace Concerter.ViewModels;

public class MyRewardsViewModel : ViewModelBase
{
    public MyRewardsViewModel()
    {
        Rewards.Add(new Giving { Place = 1, Prize = 1000 });
        Back = ReactiveCommand.Create(() => { MainWindowViewModel.Window.Content = new ArtistMyEventsViewModel();});
    }

    public ObservableCollection<Giving> Rewards { get; } = new();

    public ReactiveCommand<Unit, Unit> Back { get; }
}