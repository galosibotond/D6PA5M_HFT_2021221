using System;
using System.IO;
using System.Text.Json;
using System.Threading;
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

        protected void SerializeIntoJSON(object value, Type inputType, string jsonName)
        {
            string serializedJson = JsonSerializer.Serialize(value, inputType);

            string jsonFileName = jsonName + "_" + Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".json";

            if (File.Exists(jsonFileName))
            {
                File.Delete(jsonFileName);
            }

            using (StreamWriter streamWriter = new StreamWriter(jsonFileName))
            {
                streamWriter.Write(serializedJson);
            }

            Console.WriteLine($"{jsonFileName} has been created for request.");

            ReturnToMainMenu();
        }
    }
}
