using System.Collections.Generic;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // For testing purposes - Logic and Repository layer usages will be removed.
            AlbumStoreDbContext dbContext = new AlbumStoreDbContext();

            IArtistRepository artistRepo = new ArtistRepository(dbContext);
            IAlbumRepository albumRepo = new AlbumRepository(dbContext);
            IGenreRepository genreRepo = new GenreRepository(dbContext);
            IRecordCompanyRepository recordCompanyRepo = new RecordCompanyRepository(dbContext);

            IArtistLogic artistLogic = new ArtistLogic(artistRepo);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepo);
            IGenreLogic genreLogic = new GenreLogic(genreRepo);
            IRecordCompanyLogic recordCompanyLogic = new RecordCompanyLogic(recordCompanyRepo);

            IEnumerable<Artist> artists = artistLogic.ReadAllArtists();
            IEnumerable<Album> albums = albumLogic.ReadAllAlbums();
            IEnumerable<Genre> genres = genreLogic.ReadAllGenres();
            IEnumerable<RecordCompany> recordCompanies = recordCompanyLogic.ReadAllRecordCompanies();

            ;
        }
    }
}
