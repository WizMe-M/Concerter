using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class GenresViewModel:ViewModelBase
{
    public GenresViewModel()
    {
        Genres = new ObservableCollection<Genre>(Genre.GetBuildings());
    }

    public ObservableCollection<Genre> Genres { get; }

    [Reactive]
    public CulturalBuilding Selected { get; set; }
}