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

            ClientHelper clientHelper = new ClientHelper(consoleMenu, restService);

            clientHelper.ShowConsoleMenu();
        }
    }
}
