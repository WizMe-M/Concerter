using System;
using System.Collections.ObjectModel;
using Concerter.Models;
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

        Genres.Add(new Genre { Name = "Рок'н'ролл" });
        Genres.Add(new Genre { Name = "Джаз" });
        Genres.Add(new Genre { Name = "Глэм-метал" });
        Genres.Add(new Genre { Name = "Панк-рок" });
        Genres.Add(new Genre { Name = "Гранж" });

        CulturalBuildings.Add(new CulturalBuilding
        {
            Name = "Бар \"Грибная поляна\"",
            Address = "353051, Ульяновская область, город Кашира, ул. Ломоносова, 59",
            Capacity = 200
        });
    }

    [Reactive]
    public DateTimeOffset SelectedDate { get; set; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public decimal Price { get; set; }

    public ObservableCollection<Genre> Genres { get; } = new();


    [Reactive]
    public Genre SelectedGenre { get; set; }

    public ObservableCollection<CulturalBuilding> CulturalBuildings { get; } = new();


    [Reactive]
    public CulturalBuilding SelectedBuilding { get; set; }

    [Reactive]
    public string Description { get; set; }
}