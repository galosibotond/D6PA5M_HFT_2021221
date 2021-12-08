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
        private DeleteAPIHelper deleteAPIHelper;
        private StatAPIHelper statAPIHelper;

        public ClientHelper(CreateAPIHelper createAPIHelper, 
                            ReadAPIHelper readAPIHelper, 
                            UpdateAPIHelper updateAPIHelper,
                            DeleteAPIHelper deleteAPIHelper,
                            StatAPIHelper statAPIHelper,
                            ConsoleMenu consoleMenu, 
                            RestService restService) : base(consoleMenu)
        {
            this.createAPIHelper = createAPIHelper;
            this.readAPIHelper = readAPIHelper;
            this.updateAPIHelper = updateAPIHelper;
            this.deleteAPIHelper = deleteAPIHelper;
            this.statAPIHelper = statAPIHelper;
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

            ConsoleMenu.Add(" >> DELETE ARTIST", () => deleteAPIHelper.DeleteArtist());
            ConsoleMenu.Add(" >> DELETE ALBUM", () => deleteAPIHelper.DeleteAlbum());
            ConsoleMenu.Add(" >> DELETE GENRE", () => deleteAPIHelper.DeleteGenre());
            ConsoleMenu.Add(" >> DELETE RECORD COMPANY", () => deleteAPIHelper.DeleteRecordCompany());

            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE", 
                            () => statAPIHelper.GetAverageAlbumPrice());
            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY GENRES", 
                            () => statAPIHelper.GetAverageAlbumPriceByGenres());
            ConsoleMenu.Add(" >> GET AVERAGE ALBUM PRICE BY RECORD COMPANIES", 
                            () => statAPIHelper.GetAverageAlbumPriceByRecordCompanies());
            ConsoleMenu.Add(" >> GET ALBUM COUNT BY COUNTRY", 
                            () => statAPIHelper.GetAlbumCountByCountry());
            ConsoleMenu.Add(" >> GET OVERALL STOCK BY ARTISTS", 
                            () => statAPIHelper.GetOverallStockByArtists());
            ConsoleMenu.Add(" >> GET MOST UNSELLED ALBUM BY ARTISTS", 
                            () => statAPIHelper.GetMostUnselledAlbumByArtists());
                            
            ConsoleMenu.Add(" >> EXIT", ConsoleMenu.Close);
        }
    }
}
