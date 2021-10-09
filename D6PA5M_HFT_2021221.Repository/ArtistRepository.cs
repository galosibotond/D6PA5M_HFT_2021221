using System.Linq;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        AlbumStoreDbContext db;
        public ArtistRepository(AlbumStoreDbContext db)
        {
            this.db = db;
        }

        public void Create(Artist artist)
        {
            db.Artists.Add(artist);
            db.SaveChanges();
        }

        public Artist Read(int id)
        {
            return db.Artists.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Artist> ReadAll()
        {
            return db.Artists;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Artist artist)
        {
            var oldArtist = Read(artist.Id);
            oldArtist.Id = artist.Id;
            oldArtist.Name = artist.Name;
            db.SaveChanges();
        }
    }
}
