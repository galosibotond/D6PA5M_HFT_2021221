using System.Collections.Generic;
using ConsoleTools;

namespace D6PA5M_HFT_2021221.Client
{
    public sealed class StatAPIHelper : ConsoleActionHelperBase
    {
        private RestService restService;

        public StatAPIHelper(ConsoleMenu consoleMenu, RestService restService) : base(consoleMenu)
        {
            this.restService = restService;
        }

        public void GetAverageAlbumPriceByRecordCompanies()
        {
            string requestName = "avgalbumpricebyrecordcompany";

            List<KeyValuePair<string, double>> avgAlbumPriceByRecordCompanies =
                restService.Get<KeyValuePair<string, double>>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPriceByRecordCompanies, avgAlbumPriceByRecordCompanies.GetType(), requestName);
        }

        public void GetMostUnselledAlbumByArtists()
        {
            string requestName = "mostunselledalbum";

            List<KeyValuePair<string, string>> mostUnselledAlbums =
                        restService.Get<KeyValuePair<string, string>>($"stat/{requestName}");

            SerializeIntoJSON(mostUnselledAlbums, mostUnselledAlbums.GetType(), requestName);
        }

        public void GetOverallStockByArtists()
        {
            string requestName = "stockbyartist";

            List<KeyValuePair<string, int>> stockByArtists =
                        restService.Get<KeyValuePair<string, int>>($"stat/{requestName}");

            SerializeIntoJSON(stockByArtists, stockByArtists.GetType(), requestName);
        }

        public void GetAlbumCountByCountry()
        {
            string requestName = "albumscountbycountry";

            List<KeyValuePair<string, int>> albumsCountByCountry =
                        restService.Get<KeyValuePair<string, int>>($"stat/{requestName}");

            SerializeIntoJSON(albumsCountByCountry, albumsCountByCountry.GetType(), requestName);
        }

        public void GetAverageAlbumPriceByGenres()
        {
            string requestName = "avgalbumpricebygenre";

            List<KeyValuePair<string, double>> avgAlbumPriceByGenre =
                        restService.Get<KeyValuePair<string, double>>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPriceByGenre, avgAlbumPriceByGenre.GetType(), requestName);
        }

        public void GetAverageAlbumPrice()
        {
            string requestName = "avgalbumprice";

            double avgAlbumPrice = restService.GetSingle<double>($"stat/{requestName}");

            SerializeIntoJSON(avgAlbumPrice, avgAlbumPrice.GetType(), requestName);
        }
    }
}
