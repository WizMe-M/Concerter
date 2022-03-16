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
        EventName = "Диана Арбенина и «Ночные снайперы»";
        Description = "Встретим весну по-снайперски! 7 марта Диана Арбенина, группа \"Ночные Снайперы\" с большим" +
                      " электрическим концертом. Легенда рока, бескомпромиссная и влюбленная в дело своей жизни и " +
                      "армию своих поклонников. Способная обнять всех и заглянуть в глаза каждому! Дух рок-н-ролла, " +
                      "воплощенный Арбениной, изначально безграничный, но в ее исполнении подлинно свободный, " +
                      "балансирующий на грани нежности и неудержимости, испытает на прочность стены " +
                      "Adrenaline Stadium. Присоединяйтесь! ;)";
        Genre = "Рок-н-Ролл";
        Cost = 200;
        Date = DateOnly.FromDateTime(DateTime.Today);
        Time = TimeOnly.FromDateTime(DateTime.Now);
        CultureBuilding = "Adrenaline Stadium";
        Address = "Москва, Ленинградский просп., 80, корп. 17";
    }

    public EventViewModel(Event @event)
    {
        Id = @event.Id;
        EventName = @event.Name;
        Description = @event.Description;
        Cost = @event.Cost;
        Genre = @event.Genre.Name;
        Date = @event.Date;
        Time = @event.Time;
        CultureBuilding = @event.CulturalBuilding.Name;
        Address = @event.CulturalBuilding.Address;
    }

    [Reactive]
    public int Id { get; set; }

    [Reactive]
    public string EventName { get; set; }

    [Reactive]
    public string Genre { get; set; }

    [Reactive]
    public decimal Cost { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public DateOnly Date { get; set; }

    [Reactive]
    public TimeOnly Time { get; set; }

    [Reactive]
    public string CultureBuilding { get; set; }

    [Reactive]
    public string Address { get; set; }
}