using System;
using System.Net.Http;
using ConsoleTools;

namespace D6PA5M_HFT_2021221.Client
{
    public sealed class DeleteAPIHelper : ConsoleActionHelperBase
    {
        private RestService restService;

        public DeleteAPIHelper(ConsoleMenu consoleMenu, RestService restService) : base(consoleMenu)
        {
            this.restService = restService ?? throw new ArgumentNullException(nameof(restService));
        }

        public void DeleteRecordCompany()
        {
            string requestName = "recordcompany";

            DeleteEntity(requestName);
        }

        public void DeleteGenre()
        {
            string requestName = "genre";

            DeleteEntity(requestName);
        }

        public void DeleteAlbum()
        {
            string requestName = "album";

            DeleteEntity(requestName);
        }

        public void DeleteArtist()
        {
            string requestName = "artist";

            DeleteEntity(requestName);
        }

        public void DeleteEntity(string requestName)
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
    }
}
