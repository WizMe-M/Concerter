using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class ArtistEventInfoViewModel :ViewModelBase
{
    public ArtistEventInfoViewModel()
    {
        //init
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = null });
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = "" });
        Artists.Add(new User() { FirstName = "Андрей", LastName = "Андреев", MiddleName = "Андреевич" });
        Name = "День рождения Иоганна Себастьяна Баха";
        Price = 200;
        CulturalBinding = "Бар \"Грибная поляна\"";
        Address = "353051, Ульяновская область, город Кашира, ул. Ломоносова, 59";
        Description =
            @"За последний год проект ЛСП успел неоднократно взлететь в топ-чарты, выпустить космические релизы и отыграть концерты по всей стране, включая два столичных солдаута в MMD.

Смертельно романтичный бэнд снова вывернет наизнанку твою душу на сцене Music Media Dome, продолжив традицию больших весенних концертов.";
        Impresario = "Иванов Иван Иванович";
    }

    public ObservableCollection<User> Artists { get; } = new();

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public decimal Price { get; set; }

    [Reactive]
    public string Impresario { get; set; }

    [Reactive]
    public string CulturalBinding { get; set; }

    [Reactive]
    public string Address { get; set; }
}