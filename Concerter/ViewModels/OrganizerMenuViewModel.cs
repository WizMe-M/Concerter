using System.Windows.Input;
using ReactiveUI;

namespace Concerter.ViewModels;

public class OrganizerMenuViewModel : ViewModelBase
{
    public OrganizerMenuViewModel()
    {
        OpenEvents = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerEventsViewModel();
        });
        
        OpenBuildings = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new CulturalBuildingsViewModel();
        });
        
        OpenUsers = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new AllUserViewModel();
        });
        
        OpenGenres = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new GenresViewModel();
        });
        
        OpenExport = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new ExportSellDataViewModel();
        });
    }

    public ICommand OpenEvents { get; }
    public ICommand OpenBuildings { get; }
    public ICommand OpenUsers { get; }
    public ICommand OpenGenres { get; }
    public ICommand OpenExport { get; }
}