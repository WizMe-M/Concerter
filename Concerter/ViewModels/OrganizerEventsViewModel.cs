using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Concerter.Models;
using Concerter.Views;
using MessageBox.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Type = Concerter.Models.Type;

namespace Concerter.ViewModels;

public class OrganizerEventsViewModel : ViewModelBase
{
    private int _foundIndex = -1;

    public OrganizerEventsViewModel()
    {
        IsAscendingSort = true;
        EventList = new EventListViewModel(Event.GetEvents(), RoleAccess.Organizer);
        
        FilterTypes = new ObservableCollection<Type>(Type.GetTypes());
        FilterGenres = new ObservableCollection<Genre>(Genre.GetGenres());
        FilterImpresario = new ObservableCollection<User>(User.GetImpresario());
        FilterStatuses = new ObservableCollection<Status>(Status.GetStatuses());
        SortingParameters = new ObservableCollection<string>(new[]
        {
            "Время",
            "Название",
            "Стоимость"
        });

        this.WhenAnyValue(model => model.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(Find!);

        ClearSearch = ReactiveCommand.Create(() =>
        {
            _foundIndex = -1;
            EventList.SelectedEvent = null!;
            SearchText = "";
        });

        Menu = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel();
        });

        AddEvent = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new AddEventViewModel();
        });
    }

    public OrganizerEventsViewModel(DateOnly date) : this()
    {
        EventList = new EventListViewModel(Event.GetEvents(), date, RoleAccess.Organizer);
    }

    private void Find(string s)
    {
        var events = EventList.SelectedDateEvents?.ToArray();
        if (events is null) return;

        EventViewModel found = null!;

        if (string.IsNullOrWhiteSpace(s)) return;
        for (var i = 0; i < events.Length; i++)
        {
            if (i <= _foundIndex) continue;

            var e = events[i];
            if (e.Name.Contains(s))
            {
                found = e;
                break;
            }
        }

        if (found is null)
        {
            MessageBoxManager.GetMessageBoxStandardWindow("",
                "Не найдено мероприятие с таким названием").Show();
            _foundIndex = -1;
            return;
        }

        EventList.SelectedEvent = found;
    }

    [Reactive]
    public EventListViewModel EventList { get; set; }

    [Reactive]
    public string? SearchText { get; set; }

    [Reactive]
    public bool IsAscendingSort { get; set; }

    [Reactive]
    public int SelectedSortParameterId { get; set; }

    [Reactive]
    public Type SelectedFilterType { get; set; }

    [Reactive]
    public Status SelectedFilterStatus { get; set; }

    [Reactive]
    public User SelectedFilterImpresario { get; set; }

    [Reactive]
    public Genre SelectedFilterGenre { get; set; }

    public ObservableCollection<Type> FilterTypes { get; }
    public ObservableCollection<Status> FilterStatuses { get; }
    public ObservableCollection<User> FilterImpresario { get; }
    public ObservableCollection<Genre> FilterGenres { get; }
    public ObservableCollection<string> SortingParameters { get; }

    public ICommand ClearSearch { get; }
    public ICommand Menu { get; }
    public ICommand AddEvent { get; }
}