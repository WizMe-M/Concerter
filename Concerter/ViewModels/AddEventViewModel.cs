using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class AddEventViewModel : ViewModelBase
{
    public AddEventViewModel()
    {
        SelectedDate = DateTime.Today;
        Name = "День рождения Иоганна Себастьяна Баха";
        Price = 200;
        Description =
            @"За последний год проект ЛСП успел неоднократно взлететь в топ-чарты, выпустить космические релизы и отыграть концерты по всей стране, включая два столичных солдаута в MMD.

Смертельно романтичный бэнд снова вывернет наизнанку твою душу на сцене Music Media Dome, продолжив традицию больших весенних концертов.";

        Genres = new ObservableCollection<Genre>(Genre.GetGenres());
        CulturalBuildings = new ObservableCollection<CulturalBuilding>(CulturalBuilding.GetBuildings());
        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel();
        });
    }

    [Reactive]
    public DateTimeOffset SelectedDate { get; set; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public decimal Price { get; set; }

    public ObservableCollection<Genre> Genres { get; }


    [Reactive]
    public Genre SelectedGenre { get; set; }

    public ObservableCollection<CulturalBuilding> CulturalBuildings { get; }


    [Reactive]
    public CulturalBuilding SelectedBuilding { get; set; }

    [Reactive]
    public string Description { get; set; }

    public ICommand Back { get; }

    public ICommand Add { get; }
}