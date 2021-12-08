using System;
using System.Collections.Generic;
using ConsoleTools;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Client
{
    public sealed class ReadAPIHelper : ConsoleActionHelperBase
    {
        private RestService restService;

        public ReadAPIHelper(ConsoleMenu consoleMenu, RestService restService) : base(consoleMenu)
        {
            this.restService = restService;
        }

        public void ReadRecordCompany()
        {
            string requestName = "recordcompany";

            ReadEntity<RecordCompany>(requestName);
        }

        public void ReadAlbum()
        {
            string requestName = "album";

            ReadEntity<Album>(requestName);
        }

        public void ReadGenre()
        {
            string requestName = "genre";

            ReadEntity<Genre>(requestName);
        }

        public void ReadArtist()
        {
            string requestName = "artist";

            ReadEntity<Artist>(requestName);
        }

        public void ReadEntity<T>(string requestName)
        {
            Console.Write("\nPlease type the ID of the object you want to read and press Enter: ");

            string inputFromConsole = Console.ReadLine();

            int id;

            if (!int.TryParse(inputFromConsole, out id))
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

        public void ReadAllRecordCompanies()
        {
            string requestName = "recordcompany";

            ReadAllEntites<RecordCompany>(requestName);
        }

        public void ReadAllGenres()
        {
            string requestName = "genre";

            ReadAllEntites<Genre>(requestName);
        }

        public void ReadAllAlbums()
        {
            string requestName = "album";

            ReadAllEntites<Album>(requestName);
        }

        public void ReadAllArtists()
        {
            string requestName = "artist";

            ReadAllEntites<Artist>(requestName);
        }

        public void ReadAllEntites<T>(string requestName)
        {
            List<T> entities =
                restService.Get<T>($"{requestName}");

            SerializeIntoJSON(entities, entities.GetType(), requestName);
        }
    }
}
