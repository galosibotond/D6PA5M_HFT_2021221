using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using ConsoleTools;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Client
{
    public class ClientHelper
    {
        private ConsoleMenu consoleMenu;
        private RestService restService;

        public ClientHelper(ConsoleMenu consoleMenu, RestService restService)
        {
            this.consoleMenu = consoleMenu;
            this.restService = restService;

            CreateConsoleMenu();
        }

        private void CreateConsoleMenu()
        {
            consoleMenu.Add(" >> CREATE ARTIST", () => CreateArtist());
            consoleMenu.Add(" >> CREATE ALBUM", () => CreateAlbum());
            consoleMenu.Add(" >> CREATE GENRE", () => CreateGenre());
            consoleMenu.Add(" >> CREATE RECORD COMPANY", () => CreateRecordCompany());

            consoleMenu.Add(" >> READ ARTIST BY ID", () => ReadArtist());
            consoleMenu.Add(" >> READ ALBUM BY ID", () => ReadAlbum());
            consoleMenu.Add(" >> READ GENRE BY ID", () => ReadGenre());
            consoleMenu.Add(" >> READ RECORD COMPANY BY ID", () => ReadRecordCompany());

            consoleMenu.Add(" >> READ ALL ARTISTS", () => ReadAllArtists());
            consoleMenu.Add(" >> READ ALL ALBUMS", () => ReadAllAlbums());
            consoleMenu.Add(" >> READ ALL GENRES", () => ReadAllGenres());
            consoleMenu.Add(" >> READ ALL RECORD COMPANIES", () => ReadAllRecordCompanies());

            consoleMenu.Add(" >> UPDATE ARTIST", () => UpdateArtist());
            consoleMenu.Add(" >> UPDATE ALBUM", () => UpdateAlbum());
            consoleMenu.Add(" >> UPDATE GENRE", () => UpdateGenre());
            consoleMenu.Add(" >> UPDATE RECORD COMPANY", () => UpdateRecordCompany());

            consoleMenu.Add(" >> DELETE ARTIST", () => DeleteArtist());
            consoleMenu.Add(" >> DELETE ALBUM", () => DeleteAlbum());
            consoleMenu.Add(" >> DELETE GENRE", () => DeleteGenre());
            consoleMenu.Add(" >> DELETE RECORD COMPANY", () => DeleteRecordCompany());

            consoleMenu.Add(" >> GET AVERAGE ALBUM PRICE", () => GetAverageAlbumPrice());
            consoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY GENRES", () => GetAverageAlbumPriceByGenres());
            consoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY RECORD COMPANIES", () => GetAverageAlbumPriceByRecordCompanies());
            consoleMenu.Add(" >> GET ALBUM COUNT BY COUNTRY", () => GetAlbumCountByCountry());
            consoleMenu.Add(" >> GET OVERALL STOCK BY ARTISTS", () => GetOverallStockByArtists());
            consoleMenu.Add(" >> GET MOST UNSELLED ALBUM BY ARTISTS", () => GetMostUnselledAlbumByArtists());
                            
            consoleMenu.Add(" >> EXIT", ConsoleMenu.Close);
        }

        public void ShowConsoleMenu()
        {
            consoleMenu.Show();
        }

        private void ReturnToMainMenu()
        {
            Thread.Sleep(3000);

            ShowConsoleMenu();
        }

        private void CreateAlbum()
        {
            string requestName = "album";

            Console.Clear();
            Console.Write("\nPlease enter the title of the album: ");

            string title = Console.ReadLine();

            Console.Write("\n\nPlease enter the price of the album: ");
            string inputPrice = Console.ReadLine();

            int price;

            if (!int.TryParse(inputPrice, out price))
            {
                Console.Write("\n\nInvalid price has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the stock of the album: ");
            string inputStock = Console.ReadLine();

            int stock;

            if (!int.TryParse(inputStock, out stock))
            {
                Console.Write("\n\nInvalid stock has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the ID of the artist of the album: ");
            string inputArtistId = Console.ReadLine();

            int artistId;

            if (!int.TryParse(inputArtistId, out artistId))
            {
                Console.Write("\n\nInvalid artist ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Artist artist = restService.GetSingle<Artist>($"artist\\{artistId}");

            if (artist == null)
            {
                Console.Write("\n\nThere is no artist with this ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the ID of the record company of the album: ");
            string inputRecordCompanyId = Console.ReadLine();

            int recordCompanyId;

            if (!int.TryParse(inputRecordCompanyId, out recordCompanyId))
            {
                Console.Write("\n\nInvalid record company ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            RecordCompany recordCompany = restService.GetSingle<RecordCompany>($"recordcompany\\{recordCompanyId}");

            if (recordCompany == null)
            {
                Console.Write("\n\nThere is no record company with this ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Album albumToCreate = new Album()
            {
                Title = title,
                Price = price,
                Stock = stock,
                ArtistId = artistId,
                RecordCompanyId = recordCompanyId
            };

            try
            {
                restService.Post<Album>(albumToCreate, requestName);
            }
            catch (ArgumentException)
            {

                Console.WriteLine("Invalid or empty title for album, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nThe album has been created!");

            ReturnToMainMenu();

            consoleMenu.Show();
        }

        private void CreateGenre()
        {
            string requestName = "genre";

            Console.Clear();
            Console.Write("\nPlease enter the name of the genre: ");

            string genreName = Console.ReadLine();

            Genre genreToCreate = new Genre()
            {
                Name = genreName
            };

            try
            {
                restService.Post<Genre>(genreToCreate, requestName);
            }
            catch (ArgumentException) 
            { 
                Console.WriteLine("Invalid or empty name for genre, please try again!");

                ReturnToMainMenu();

                return; 
            }

            Console.WriteLine("The genre has been created!");

            ReturnToMainMenu();
        }

        private void CreateArtist()
        {
            string requestName = "artist";

            Console.Clear();
            Console.Write("\nPlease enter the name of the artist: ");

            string artistName = Console.ReadLine();

            Console.Write("\n\nPlease enter the country of the artist: ");
            string countryName = Console.ReadLine();

            if (string.IsNullOrEmpty(countryName))
            {
                Console.Write("\n\nIvalid or empty country name has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the ID of the genre of the artist: ");
            string inputGenreId = Console.ReadLine();

            int genreId;

            if (!int.TryParse(inputGenreId, out genreId))
            {
                Console.Write("\n\nInvalid genre ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Genre genre = restService.GetSingle<Genre>($"genre\\{genreId}");

            if (genre == null)
            {
                Console.Write("\n\nThere is no genre with this ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Artist artistToCreate = new Artist()
            {
                Name = artistName,
                Country = countryName,
                GenreId = genreId
            };

            try
            {
                restService.Post<Artist>(artistToCreate, requestName);
            }
            catch (ArgumentException)
            {

                Console.WriteLine("Invalid or empty name for artist, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.WriteLine("The artist has been created!");

            ReturnToMainMenu();
        }

        private void CreateRecordCompany()
        {
            string requestName = "recordcompany";

            Console.Clear();
            Console.Write("\nPlease enter the name of the record company: ");

            string recordCompanyName = Console.ReadLine();

            RecordCompany recordCompanyToCreate = new RecordCompany()
            {
                Name = recordCompanyName
            };

            try
            {
                restService.Post<RecordCompany>(recordCompanyToCreate, requestName);
            }
            catch (ArgumentException)
            {

                Console.WriteLine("Invalid or empty name for record company, please try again!");

                ReturnToMainMenu();

                return;
            }
            
            Console.WriteLine("The record company has been created!");

            Thread.Sleep(3000);

            consoleMenu.Show();
        }

        private void ReadRecordCompany()
        {
            string requestName = "recordcompany";

            ReadEntity<RecordCompany>(requestName);
        }

        private void ReadAlbum()
        {
            string requestName = "album";

            ReadEntity<Album>(requestName);
        }

        private void ReadGenre()
        {
            string requestName = "genre";

            ReadEntity<Genre>(requestName);
        }

        private void ReadArtist()
        {
            string requestName = "artist";

            ReadEntity<Artist>(requestName);
        }

        private void ReadEntity<T>(string requestName)
        {
            Console.Write("\nPlease type the ID of the object you want to read and press Enter: ");

            string inputFromConsole = Console.ReadLine();

            int id;

            if(!int.TryParse(inputFromConsole, out id))
            {
                Console.WriteLine("Please type a valid ID!");

                ReturnToMainMenu();

                return;
            }

            T requestedObject =
                restService.GetSingle<T>($"{requestName}\\{id}");

            if (requestedObject == null)
            {
                Console.WriteLine("There is no object with this ID, please try again!");

                ReturnToMainMenu();
            }

            SerializeIntoJSON(requestedObject, requestedObject.GetType(), requestName);
        }

        private void ReadAllRecordCompanies()
        {
            string requestName = "recordcompany";

            ReadAllEntites<RecordCompany>(requestName);
        }

        private void ReadAllGenres()
        {
            string requestName = "genre";

            ReadAllEntites<Genre>(requestName);
        }

        private void ReadAllAlbums()
        {
            string requestName = "album";

            ReadAllEntites<Album>(requestName);
        }

        private void ReadAllArtists()
        {
            string requestName = "artist";

            ReadAllEntites<Artist>(requestName);
        }

        private void ReadAllEntites<T>(string requestName)
        {
            List<T> entities =
                restService.Get<T>($"{requestName}");

            SerializeIntoJSON(entities, entities.GetType(), requestName);
        }

        private void UpdateRecordCompany()
        {
            string requestName = "recordcompany";

            Console.Write("\nPlease enter the ID of the record company you want to update: ");
            string inputId = Console.ReadLine();

            int id;

            if (!int.TryParse(inputId, out id))
            {
                Console.WriteLine("\n\nInvalid ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            RecordCompany recordCompany =
                restService.GetSingle<RecordCompany>($"{requestName}\\{id}");

            if (recordCompany == null)
            {
                Console.WriteLine("\n\nThere is no record company with that ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new name of the record company: ");
            string inputName = Console.ReadLine();

            if (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("\n\nInvalid or empty name for record company, please try again!");

                ReturnToMainMenu();

                return;
            }

            RecordCompany recordCompanyToUpdate = new RecordCompany()
            {
                Name = inputName,
                Id = id
            };

            restService.Put<RecordCompany>(recordCompanyToUpdate, requestName);

            Console.Write($"The record company with ID {id} has been updated!");

            ReturnToMainMenu();
        }

        private void UpdateGenre()
        {
            string requestName = "genre";

            Console.Write("\nPlease enter the ID of the genre you want to update: ");
            string inputId = Console.ReadLine();

            int id;

            if (!int.TryParse(inputId, out id))
            {
                Console.WriteLine("\n\nInvalid ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Genre genre =
                restService.GetSingle<Genre>($"{requestName}\\{id}");

            if (genre == null)
            {
                Console.WriteLine("\n\nThere is no genre with that ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new name of the genre: ");
            string inputName = Console.ReadLine();

            if (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("\n\nInvalid or empty name for genre, please try again!");

                ReturnToMainMenu();

                return;
            }

            Genre genreToUpdate = new Genre()
            {
                Name = inputName,
                Id = id,
            };

            restService.Put<Genre>(genreToUpdate, requestName);

            Console.Write($"The genre with ID {id} has been updated!");

            ReturnToMainMenu();
        }

        private void UpdateAlbum()
        {
            string requestName = "album";

            Console.Write("\nPlease enter the ID of the album you want to update: ");
            string inputId = Console.ReadLine();

            int id;

            if (!int.TryParse(inputId, out id))
            {
                Console.WriteLine("\n\nInvalid ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Album album =
                restService.GetSingle<Album>($"{requestName}\\{id}");

            if (album == null)
            {
                Console.WriteLine("\n\nThere is no album with that ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new title of the album: ");
            string inputName = Console.ReadLine();

            if (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("\n\nInvalid or empty name for album title, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new stock: ");
            string inputStock = Console.ReadLine();

            int stock;

            if (!int.TryParse(inputStock, out stock))
            {
                Console.WriteLine("\n\nInvalid stock has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new price: ");
            string inputPrice = Console.ReadLine();

            int price;

            if (!int.TryParse(inputPrice, out price))
            {
                Console.WriteLine("\n\nInvalid price has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Album albumToUpdate = new Album()
            {
                Title = inputName,
                Id = id,
                Stock = stock,
                Price = price
            };

            restService.Put<Album>(albumToUpdate, requestName);

            Console.Write($"The album with ID {id} has been updated!");

            ReturnToMainMenu();
        }

        private void UpdateArtist()
        {
            string requestName = "artist";

            Console.Write("\nPlease enter the ID of the artist you want to update: ");
            string inputId = Console.ReadLine();

            int id;

            if (!int.TryParse(inputId, out id))
            {
                Console.WriteLine("\n\nInvalid ID has been entered, please try again!");

                ReturnToMainMenu();

                return;
            }

            Artist artist =
                restService.GetSingle<Artist>($"{requestName}\\{id}");

            if (artist == null)
            {
                Console.WriteLine("\n\nThere is no artist with that ID, please try again!");

                ReturnToMainMenu();

                return;
            }

            Console.Write("\n\nPlease enter the new name of the artist: ");
            string inputName = Console.ReadLine();

            if (string.IsNullOrEmpty(inputName))
            {
                Console.WriteLine("\n\nInvalid or empty name for artist, please try again!");

                ReturnToMainMenu();

                return;
            }

            Artist artistToUpdate = new Artist()
            {
                Name = inputName,
                Id = id
            };

            restService.Put<Artist>(artistToUpdate, requestName);

            Console.Write($"The artist with ID {id} has been updated!");

            ReturnToMainMenu();
        }

        private void DeleteRecordCompany()
        {
            string requestName = "recordcompany";

            DeleteEntity(requestName);
        }

        private void DeleteGenre()
        {
            string requestName = "genre";

            DeleteEntity(requestName);
        }

        private void DeleteAlbum()
        {
            string requestName = "album";

            DeleteEntity(requestName);
        }

        private void DeleteArtist()
        {
            string requestName = "artist";

            DeleteEntity(requestName);
        }

        private void DeleteEntity(string requestName)
        {
            Console.Write("\nPlease type the ID of the object you want to delete and press Enter: ");

            string inputFromConsole = Console.ReadLine();

            int id;

            if (!int.TryParse(inputFromConsole, out id))
            {
                Console.WriteLine("Please type a valid ID!");

                ReturnToMainMenu();

                return;
            }

            try
            {
                restService.Delete(id, requestName);
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("There is no object with this ID, please try again!");
            }
            finally
            {
                ReturnToMainMenu();
            }
        }

        private void GetAverageAlbumPriceByRecordCompanies()
        {
            string requestName = "avgalbumpricebyrecordcompany";

            List<KeyValuePair<string, double>> avgAlbumPriceByRecordCompanies = 
                restService.Get<KeyValuePair<string, double>>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPriceByRecordCompanies, avgAlbumPriceByRecordCompanies.GetType(), requestName);
        }

        private void GetMostUnselledAlbumByArtists()
        {
            string requestName = "mostunselledalbum";

            List<KeyValuePair<string, string>> mostUnselledAlbums =
                        restService.Get<KeyValuePair<string, string>>($"stat/{requestName}");

            SerializeIntoJSON(mostUnselledAlbums, mostUnselledAlbums.GetType(), requestName);
        }

        private void GetOverallStockByArtists()
        {
            string requestName = "stockbyartist";

            List<KeyValuePair<string, int>> stockByArtists =
                        restService.Get<KeyValuePair<string, int>>($"stat/{requestName}");

            SerializeIntoJSON(stockByArtists, stockByArtists.GetType(), requestName);
        }

        private void GetAlbumCountByCountry()
        {
            string requestName = "albumscountbycountry";

            List <KeyValuePair<string, int>> albumsCountByCountry =
                        restService.Get<KeyValuePair<string, int>>($"stat/{requestName}");

            SerializeIntoJSON(albumsCountByCountry, albumsCountByCountry.GetType(), requestName);
        }

        private void GetAverageAlbumPriceByGenres()
        {
            string requestName = "avgalbumpricebygenre";

            List<KeyValuePair<string, double>> avgAlbumPriceByGenre =
                        restService.Get<KeyValuePair<string, double>>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPriceByGenre, avgAlbumPriceByGenre.GetType(), requestName);
        }

        private void GetAverageAlbumPrice()
        {
            string requestName = "avgalbumprice";

            double avgAlbumPrice = restService.GetSingle<double>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPrice, avgAlbumPrice.GetType(), requestName);
        }

        private void SerializeIntoJSON(object value, Type inputType, string jsonName)
        {
            string serializedJson = JsonSerializer.Serialize(value, inputType);

            string jsonFileName = jsonName + "_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".json";

            if (File.Exists(jsonFileName))
            {
                File.Delete(jsonFileName);
            }

            using (StreamWriter streamWriter = new StreamWriter(jsonFileName))
            {
                streamWriter.Write(serializedJson);
            }

            Console.WriteLine($"{jsonFileName} has been created for request.");

            ReturnToMainMenu();
        }
    }
}
