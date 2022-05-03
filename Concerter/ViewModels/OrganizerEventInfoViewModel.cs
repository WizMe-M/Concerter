using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class OrganizerEventInfoViewModel : ViewModelBase
{
    private readonly Event _event;

    public OrganizerEventInfoViewModel()
    {
        var canToggle = this.WhenAnyValue(
            model => model.Name,
            model => model.Description,
            model => model.Cost,
            model => model.SelectedDate,
            model => model.SelectedBuilding,
            model => model.SelectedGenre,
            (name, desc, cost, date, building, genre) =>
                !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(desc)
                && cost is > 150 and < 1000000000
                && date > DateTime.Today.AddDays(1)
                && building is not null
                && genre is not null
                || !IsEditMode);

        //init
        _event = null!;
        IsEditMode = false;
        SelectedDate = DateTime.Today;
        Name = "День рождения Иоганна Себастьяна Баха";
        Cost = 200;
        Description =
            @"За последний год проект ЛСП успел неоднократно взлететь в топ-чарты, выпустить космические релизы и отыграть концерты по всей стране, включая два столичных солдаута в MMD.

Смертельно романтичный бэнд снова вывернет наизнанку твою душу на сцене Music Media Dome, продолжив традицию больших весенних концертов.";

        Genres = new ObservableCollection<Genre>(Genre.GetGenres());
        CulturalBuildings = new ObservableCollection<CulturalBuilding>(CulturalBuilding.GetBuildings());
        ChangeOrSave = ReactiveCommand.CreateFromTask(async () =>
        {
            IsEditMode = !IsEditMode;
            if (IsEditMode == false)
            {
                _event.Name = Name;
                _event.Cost = Cost;
                _event.Date = DateOnly.FromDateTime(SelectedDate.Date);
                _event.Description = Description;
                _event.GenreId = SelectedGenre!.Id;
                _event.CulturalBuildingId = SelectedBuilding!.Id;

                await _event.SaveAsync();
            }
        }, canToggle);

        Back = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel(_event.Date);
        });

        Delete = ReactiveCommand.CreateFromTask(async () =>
        {
            await _event.Delete();
            MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel(_event.Date);
        });
    }

    public OrganizerEventInfoViewModel(EventViewModel e) : this()
    {
        _event = Event.Find(e.Id);
        Name = e.Name;
        Cost = e.Cost;
        SelectedDate = new DateTimeOffset(e.Date.ToDateTime(new TimeOnly()));
        SelectedTime = e.Time.ToTimeSpan();
        Description = e.Description;
        SelectedGenre = e.EventGenre;
        SelectedBuilding = e.EventCulturalBuilding;

        AssignImpresario = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new AssignImpresarioViewModel(e);
        });
    }

    [Reactive]
    public DateTimeOffset SelectedDate { get; set; }

    [Reactive]
    public TimeSpan SelectedTime { get; set; }
    
    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public decimal Cost { get; set; }

    public ObservableCollection<Genre> Genres { get; }
    public ObservableCollection<CulturalBuilding> CulturalBuildings { get; }

    [Reactive]
    public Genre SelectedGenre { get; set; }

    [Reactive]
    public CulturalBuilding SelectedBuilding { get; set; }

    [Reactive]
    public string? Description { get; set; }

    [Reactive]
    public bool IsEditMode { get; set; }

    public ICommand ChangeOrSave { get; }
    public ICommand Delete { get; }
    public ICommand AssignImpresario { get; }
    public ICommand Back { get; }
    public ICommand ChangeStatus { get; }
}