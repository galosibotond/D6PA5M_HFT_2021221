using System.Linq;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository;

namespace D6PA5M_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // For testing purposes - Data and Repository layer usages will be removed.
            AlbumStoreDbContext dbContext = new AlbumStoreDbContext();

            ArtistRepository artistRepo = new ArtistRepository(dbContext);
            AlbumRepository albumRepo = new AlbumRepository(dbContext);
            GenreRepository genreRepo = new GenreRepository(dbContext);
            RecordCompanyRepository recordCompanyRepo = new RecordCompanyRepository(dbContext);

            IQueryable<Artist> artists = artistRepo.ReadAll();
            IQueryable<Album> albums = albumRepo.ReadAll();
            IQueryable<Genre> genres = genreRepo.ReadAll();
            IQueryable<RecordCompany> recordCompanies = recordCompanyRepo.ReadAll();

            ;
        }
    }
}
