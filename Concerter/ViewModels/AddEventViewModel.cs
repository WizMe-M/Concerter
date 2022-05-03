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
        var canAdd = this.WhenAnyValue(
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
                && date > DateTime.Today
                && building is not null
                && genre is not null);

        SelectedDate = DateTimeOffset.Now;
        Genres = new ObservableCollection<Genre>(Genre.GetGenres());
        CulturalBuildings = new ObservableCollection<CulturalBuilding>(CulturalBuilding.GetBuildings());

        Back = ReactiveCommand.Create(() => { MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel(); });

        Add = ReactiveCommand.Create(() =>
        {
            var e = new Event
            {
                Name = Name,
                Description = Description,
                Cost = Cost,
                Date = DateOnly.FromDateTime(SelectedDate.DateTime),
                CulturalBuildingId = SelectedBuilding!.Id,
                GenreId = SelectedGenre!.Id,
                StatusId = 1,
                TypeId = 1,
                Time = TimeOnly.FromTimeSpan(SelectedTime)
            };
            e.Add();
            MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel();
        }, canAdd);
    }

    [Reactive]
    public DateTimeOffset SelectedDate { get; set; }

    [Reactive]
    public TimeSpan SelectedTime { get; set; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public string Description { get; set; }

    [Reactive]
    public decimal Cost { get; set; }

    public ObservableCollection<Genre> Genres { get; }


    [Reactive]
    public Genre SelectedGenre { get; set; }

    public ObservableCollection<CulturalBuilding> CulturalBuildings { get; }


    [Reactive]
    public CulturalBuilding SelectedBuilding { get; set; }

    public ICommand Back { get; }

    public ICommand Add { get; }
}