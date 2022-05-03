using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Concerter.Models;
using DynamicData;
using MessageBox.Avalonia;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Concerter.ViewModels;

public class GenresViewModel:ViewModelBase
{
    public GenresViewModel()
    {
        Genres = new ObservableCollection<Genre>(Genre.GetGenres());
        this.WhenAnyValue(model => model.Selected)
            .Subscribe(SelectGenre);
        
        var canEditOrDelete = this.WhenAnyValue(model => model.Selected,
            selector:genre => genre is not null);

        Add = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", 
                    "Введите название жанра").Show();
                return;
            }
            
            var name = Name.Trim();
            if (Genre.Exists(name))
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", 
                    "Жанр с таким названием уже существует").Show();
                return;
            }

            Genre.Add(name);
            Genres.Clear();
            Genres.AddRange(Genre.GetGenres());
        });

        Edit = ReactiveCommand.CreateFromTask(async () =>
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", 
                    "Введите название жанра").Show();
                return;
            }
            
            var genre = Selected!;
            genre.Name = Name;
            await genre.SaveAsync();
            Genres.Clear();
            Genres.AddRange(Genre.GetGenres());
        }, canEditOrDelete);

        Delete = ReactiveCommand.Create(() =>
        {
            Genre.Delete(Selected!.Id);
            Genres.Clear();
            Genres.AddRange(Genre.GetGenres());
        }, canEditOrDelete);

        Menu = ReactiveCommand.Create(() =>
        {
            MainWindowViewModel.Instance.Content = new OrganizerMenuViewModel();
        });
    }

    public ObservableCollection<Genre> Genres { get; }

    [Reactive]
    public Genre Selected { get; set; }
    
    [Reactive]
    public string Name { get; set; }

    public ICommand Menu { get; }
    public ICommand Add { get; }
    public ICommand Edit { get; }
    public ICommand Delete { get; }

    private void SelectGenre(Genre genre)
    {
        if(genre is null) return;
        Name = genre.Name;
    }
}