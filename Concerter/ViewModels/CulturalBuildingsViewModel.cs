using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using DynamicData;
using MessageBox.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class CulturalBuildingsViewModel : ViewModelBase
{
    public CulturalBuildingsViewModel()
    {
        Buildings = new ObservableCollection<CulturalBuilding>(CulturalBuilding.GetBuildings());
        this.WhenAnyValue(model => model.Selected)
            .Subscribe(SelectBuilding);

        var canEditOrDelete = this.WhenAnyValue(model => model.Selected,
            selector: culturalBuilding => culturalBuilding is not null);

        Add = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) || Capacity is < 100 or > 10000)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Заполните все поля").Show();
                return;
            }

            var name = Name.Trim();
            var address = Address.Trim();
            if (CulturalBuilding.Exists(name))
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Здание с таким названием уже существует").Show();
                return;
            }

            var building = new CulturalBuilding
            {
                Name = name,
                Address = address,
                Capacity = Capacity
            };
            building.Add();
            Buildings.Clear();
            Buildings.AddRange(CulturalBuilding.GetBuildings());
        });

        Edit = ReactiveCommand.CreateFromTask(async () =>
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Address) || Capacity is < 100 or > 10000)
            {
                await MessageBoxManager.GetMessageBoxStandardWindow("Ошибка",
                    "Заполните все поля").Show();
                return;
            }

            var name = Name.Trim();
            var address = Address.Trim();
            var culturalBuilding = Selected!;
            culturalBuilding.Name = name;
            culturalBuilding.Address = address;
            culturalBuilding.Capacity = Capacity;
            await culturalBuilding.SaveAsync();
            Buildings.Clear();
            Buildings.AddRange(CulturalBuilding.GetBuildings());
        }, canEditOrDelete);

        Delete = ReactiveCommand.Create(() =>
        {
            CulturalBuilding.Delete(Selected!.Id);
            Buildings.Clear();
            Buildings.AddRange(CulturalBuilding.GetBuildings());
        }, canEditOrDelete);

        Menu = ReactiveCommand.Create(() => { MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel(); });
    }

    public ObservableCollection<CulturalBuilding> Buildings { get; }

    [Reactive]
    public CulturalBuilding Selected { get; set; }

    [Reactive]
    public string Name { get; set; }

    [Reactive]
    public string Address { get; set; }

    [Reactive]
    public int Capacity { get; set; }

    public ICommand Menu { get; }

    public ICommand Add { get; }

    public ICommand Edit { get; }

    public ICommand Delete { get; }

    private void SelectBuilding(CulturalBuilding building)
    {
        if (building is null) return;
        Name = building.Name;
        Address = building.Address;
        Capacity = building.Capacity;
    }
}