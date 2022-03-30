using D6PA5M_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace D6PA5M_HFT_2021221.WpfClient
{
    public class AlbumViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }


        public RestCollection<Album> AlbumCollection { get; set; }

        private Album selectedAlbum;

        public Album SelectedAlbum
        {
            get { return selectedAlbum; }
            set
            {
                if (value != null)
                {
                    selectedAlbum = new Album()
                    {
                        Title = value.Title,
                        Id = value.Id,
                        Artist = value.Artist,
                        ArtistId = value.ArtistId,
                        ReleaseDate = value.ReleaseDate,
                        Price = value.Price,
                        Stock = value.Stock,
                        RecordCompany = value.RecordCompany,
                        RecordCompanyId = value.RecordCompanyId
                    };
                    OnPropertyChanged();
                    (DeleteAlbumCommand as RelayCommand)?.NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateAlbumCommand { get; set; }

        public ICommand DeleteAlbumCommand { get; set; }

        public ICommand UpdateAlbumCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public AlbumViewModel()
        {
            if (!IsInDesignMode)
            {
                AlbumCollection = new RestCollection<Album>("http://localhost:36957/", "album", "hub");
                CreateAlbumCommand = new RelayCommand(() =>
                {
                    AlbumCollection?.Add(new Album()
                    {
                        Title = SelectedAlbum.Title,
                        ArtistId = SelectedAlbum.ArtistId,
                        RecordCompanyId = SelectedAlbum.RecordCompanyId,
                        Price = SelectedAlbum.Price,
                        Stock = SelectedAlbum.Stock,
                        ReleaseDate = SelectedAlbum.ReleaseDate
                    });
                });

                UpdateAlbumCommand = new RelayCommand(() =>
                {
                    try
                    {
                        AlbumCollection?.Update(SelectedAlbum);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                    
                });

                DeleteAlbumCommand = new RelayCommand(() =>
                {
                    AlbumCollection.Delete(SelectedAlbum.Id);
                },
                () =>
                {
                    return SelectedAlbum != null;
                });
                SelectedAlbum = new Album();
            }
            
        }
    }
}
