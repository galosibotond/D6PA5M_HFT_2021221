using D6PA5M_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace D6PA5M_HFT_2021221.WpfClient
{
    public class ArtistViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Artist> ArtistCollection { get; set; }

        private Artist selectedArtist;

        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                if (value != null)
                {
                    selectedArtist = new Artist()
                    {
                        Name = value.Name,
                        Id = value.Id,
                        Country = value.Country,
                        FoundationDate = value.FoundationDate,
                        GenreId = value.GenreId,
                        Genre = value.Genre
                    };
                    OnPropertyChanged();
                    (DeleteArtistCommand as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateArtistCommand { get; set; }

        public ICommand DeleteArtistCommand { get; set; }

        public ICommand UpdateArtistCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public ArtistViewModel()
        {
            if (!IsInDesignMode)
            {
                ArtistCollection = new RestCollection<Artist>("http://localhost:36957/", "artist", "hub");
                CreateArtistCommand = new RelayCommand(() =>
                {
                    ArtistCollection?.Add(new Artist()
                    {
                        Name = SelectedArtist.Name,
                        GenreId = 1001
                    });
                });

                UpdateArtistCommand = new RelayCommand(() =>
                {
                    try
                    {
                        ArtistCollection?.Update(SelectedArtist);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteArtistCommand = new RelayCommand(() =>
                {
                    ArtistCollection.Delete(SelectedArtist.Id);
                },
                () =>
                {
                    return SelectedArtist != null;
                });
                SelectedArtist = new Artist();
            }

        }
    }
}
