using System;
using System.Threading;
using ConsoleTools;

namespace D6PA5M_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(8000);

            RestService restService = new RestService("http://localhost:36957");

            ConsoleMenu consoleMenu = new ConsoleMenu();

            CreateAPIHelper createAPIHelper = new CreateAPIHelper(consoleMenu, restService);
            ReadAPIHelper readAPIHelper = new ReadAPIHelper(consoleMenu, restService);
            UpdateAPIHelper updateAPIHelper = new UpdateAPIHelper(consoleMenu, restService);
            DeleteAPIHelper deleteAPIHelper = new DeleteAPIHelper(consoleMenu, restService);

            ClientHelper clientHelper = new ClientHelper(
                createAPIHelper, readAPIHelper, updateAPIHelper, deleteAPIHelper,consoleMenu, restService);

            clientHelper.ShowConsoleMenu();
        }
    }
}
