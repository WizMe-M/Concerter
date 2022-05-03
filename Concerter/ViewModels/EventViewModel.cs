using System;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class EventViewModel : ViewModelBase
{
    /// <summary>
    /// Для отображения в предпросмотре View
    /// </summary>
    public EventViewModel()
    {
        Id = -1;
        Name = "Диана Арбенина и «Ночные снайперы»";
        Description = "Встретим весну по-снайперски! 7 марта Диана Арбенина, группа \"Ночные Снайперы\" с большим" +
                      " электрическим концертом. Легенда рока, бескомпромиссная и влюбленная в дело своей жизни и " +
                      "армию своих поклонников. Способная обнять всех и заглянуть в глаза каждому! Дух рок-н-ролла, " +
                      "воплощенный Арбениной, изначально безграничный, но в ее исполнении подлинно свободный, " +
                      "балансирующий на грани нежности и неудержимости, испытает на прочность стены " +
                      "Adrenaline Stadium. Присоединяйтесь! ;)";
        GenreName = "Рок-н-Ролл";
        Cost = 200;
        Date = DateOnly.FromDateTime(DateTime.Today);
        Time = TimeOnly.FromDateTime(DateTime.Now);
        BuildingName = "Adrenaline Stadium";
        Address = "Москва, Ленинградский просп., 80, корп. 17";
    }

    public EventViewModel(Event e)
    {
        Id = e.Id;
        Name = e.Name;
        Description = e.Description;
        Cost = e.Cost;
        Date = e.Date;
        Time = e.Time;

        EventGenre = e.Genre;
        GenreName = EventGenre.Name;

        EventCulturalBuilding = e.CulturalBuilding;
        BuildingName = EventCulturalBuilding.Name;
        Address = EventCulturalBuilding.Address;
    }

    [Reactive]
    public int Id { get; set; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public string GenreName { get; set; }

    [Reactive]
    public decimal Cost { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public DateOnly Date { get; set; }

    [Reactive]
    public TimeOnly Time { get; set; }

    [Reactive]
    public string BuildingName { get; set; }

    [Reactive]
    public string Address { get; set; }

    public Genre EventGenre { get; set; }
    public CulturalBuilding EventCulturalBuilding { get; set; }
}