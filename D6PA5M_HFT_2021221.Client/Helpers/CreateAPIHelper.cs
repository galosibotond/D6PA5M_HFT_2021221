using System;
using ConsoleTools;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Client
{
    public sealed class CreateAPIHelper : ConsoleActionHelperBase
    {
        private RestService restService;

        public CreateAPIHelper(ConsoleMenu consoleMenu, RestService restService) : base(consoleMenu)
        {
            this.restService = restService ?? throw new ArgumentNullException(nameof(restService));
        }

        public void CreateAlbum()
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
        }

        public void CreateGenre()
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

        public void CreateArtist()
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

        public void CreateRecordCompany()
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

            ReturnToMainMenu();
        }
    }
}
