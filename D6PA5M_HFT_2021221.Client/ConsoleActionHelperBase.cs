using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleTools;

namespace D6PA5M_HFT_2021221.Client
{
    public class ConsoleActionHelperBase
    {
        protected ConsoleMenu ConsoleMenu;

        public ConsoleActionHelperBase(ConsoleMenu consoleMenu)
        {
            ConsoleMenu = consoleMenu;
        }

        public void ShowConsoleMenu()
        {
            ConsoleMenu.Show();
        }

        protected void ReturnToMainMenu()
        {
            Thread.Sleep(3000);

            ShowConsoleMenu();
        }
    }
}
