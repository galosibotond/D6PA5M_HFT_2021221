using D6PA5M_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace D6PA5M_HFT_2021221.WpfClient
{
    public class GenreViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Genre> GenreCollection { get; set; }

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (value != null)
                {
                    selectedGenre = new Genre()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteGenreCommand as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateGenreCommand { get; set; }

        public ICommand DeleteGenreCommand { get; set; }

        public ICommand UpdateGenreCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public GenreViewModel()
        {
            if (!IsInDesignMode)
            {
                GenreCollection = new RestCollection<Genre>("http://localhost:36957/", "genre", "hub");
                CreateGenreCommand = new RelayCommand(() =>
                {
                    GenreCollection?.Add(new Genre()
                    {
                        Name = SelectedGenre.Name
                    });
                });

                UpdateGenreCommand = new RelayCommand(() =>
                {
                    try
                    {
                        GenreCollection?.Update(SelectedGenre);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteGenreCommand = new RelayCommand(() =>
                {
                    GenreCollection.Delete(SelectedGenre.Id);
                },
                () =>
                {
                    return SelectedGenre != null;
                });
                SelectedGenre = new Genre();
            }

        }
    }
}
