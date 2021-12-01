using System.Threading;

namespace D6PA5M_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(8000);

            RestService rest = new RestService("http://localhost:36957");
        }
    }
}
