using System.Collections.ObjectModel;
using Concerter.Models;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class CulturalBuildingsViewModel : ViewModelBase
{
    public CulturalBuildingsViewModel()
    {
        Buildings = new ObservableCollection<CulturalBuilding>(CulturalBuilding.GetBuildings());
    }

    public ObservableCollection<CulturalBuilding> Buildings { get; }

    [Reactive]
    public CulturalBuilding Selected { get; set; }
}