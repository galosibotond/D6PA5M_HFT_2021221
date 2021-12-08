using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTools;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Client
{
    public sealed class UpdateAPIHelper : ConsoleActionHelperBase
    {
        private RestService restService;

        public UpdateAPIHelper(ConsoleMenu consoleMenu, RestService restService) : base(consoleMenu)
        {
            this.restService = restService;
        }

        public void UpdateRecordCompany()
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

        public void UpdateGenre()
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

        public void UpdateAlbum()
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

        public void UpdateArtist()
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
    }
}
