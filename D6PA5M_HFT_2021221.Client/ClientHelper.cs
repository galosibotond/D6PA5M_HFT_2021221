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
    public sealed class ClientHelper : ConsoleActionHelperBase
    {
        private RestService restService;
        private CreateAPIHelper createAPIHelper;
        private ReadAPIHelper readAPIHelper;
        private UpdateAPIHelper updateAPIHelper;

        public ClientHelper(CreateAPIHelper createAPIHelper, 
                            ReadAPIHelper readAPIHelper, 
                            UpdateAPIHelper updateAPIHelper,
                            ConsoleMenu consoleMenu, 
                            RestService restService) : base(consoleMenu)
        {
            this.createAPIHelper = createAPIHelper;
            this.readAPIHelper = readAPIHelper;
            this.updateAPIHelper = updateAPIHelper;
            this.restService = restService;

            CreateConsoleMenu();
        }

        private void CreateConsoleMenu()
        {
            ConsoleMenu.Add(" >> CREATE ARTIST", () => createAPIHelper.CreateArtist());
            ConsoleMenu.Add(" >> CREATE ALBUM", () => createAPIHelper.CreateAlbum());
            ConsoleMenu.Add(" >> CREATE GENRE", () => createAPIHelper.CreateGenre());
            ConsoleMenu.Add(" >> CREATE RECORD COMPANY", () => createAPIHelper.CreateRecordCompany());

            ConsoleMenu.Add(" >> READ ARTIST BY ID", () => readAPIHelper.ReadArtist());
            ConsoleMenu.Add(" >> READ ALBUM BY ID", () => readAPIHelper.ReadAlbum());
            ConsoleMenu.Add(" >> READ GENRE BY ID", () => readAPIHelper.ReadGenre());
            ConsoleMenu.Add(" >> READ RECORD COMPANY BY ID", () => readAPIHelper.ReadRecordCompany());

            ConsoleMenu.Add(" >> READ ALL ARTISTS", () => readAPIHelper.ReadAllArtists());
            ConsoleMenu.Add(" >> READ ALL ALBUMS", () => readAPIHelper.ReadAllAlbums());
            ConsoleMenu.Add(" >> READ ALL GENRES", () => readAPIHelper.ReadAllGenres());
            ConsoleMenu.Add(" >> READ ALL RECORD COMPANIES", () => readAPIHelper.ReadAllRecordCompanies());

            ConsoleMenu.Add(" >> UPDATE ARTIST", () => updateAPIHelper.UpdateArtist());
            ConsoleMenu.Add(" >> UPDATE ALBUM", () => updateAPIHelper.UpdateAlbum());
            ConsoleMenu.Add(" >> UPDATE GENRE", () => updateAPIHelper.UpdateGenre());
            ConsoleMenu.Add(" >> UPDATE RECORD COMPANY", () => updateAPIHelper.UpdateRecordCompany());

            ConsoleMenu.Add(" >> DELETE ARTIST", () => DeleteArtist());
            ConsoleMenu.Add(" >> DELETE ALBUM", () => DeleteAlbum());
            ConsoleMenu.Add(" >> DELETE GENRE", () => DeleteGenre());
            ConsoleMenu.Add(" >> DELETE RECORD COMPANY", () => DeleteRecordCompany());

            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE", () => GetAverageAlbumPrice());
            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY GENRES", () => GetAverageAlbumPriceByGenres());
            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY RECORD COMPANIES", () => GetAverageAlbumPriceByRecordCompanies());
            ConsoleMenu.Add(" >> GET ALBUM COUNT BY COUNTRY", () => GetAlbumCountByCountry());
            ConsoleMenu.Add(" >> GET OVERALL STOCK BY ARTISTS", () => GetOverallStockByArtists());
            ConsoleMenu.Add(" >> GET MOST UNSELLED ALBUM BY ARTISTS", () => GetMostUnselledAlbumByArtists());
                            
            ConsoleMenu.Add(" >> EXIT", ConsoleMenu.Close);
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
    }
}
