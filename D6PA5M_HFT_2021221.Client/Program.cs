using System;
using System.Linq;
using D6PA5M_HFT_2021221.Data;

namespace D6PA5M_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // For testing purposes - project reference for D6PA5M_HFT_2021221.Data will be removed
            AlbumStoreDbContext dbContext = new AlbumStoreDbContext();

            dbContext.Artists.ToArray();
            dbContext.Albums.ToArray();
            dbContext.Genres.ToArray();
            dbContext.RecordCompanies.ToArray();

            ;
        }
    }
}
