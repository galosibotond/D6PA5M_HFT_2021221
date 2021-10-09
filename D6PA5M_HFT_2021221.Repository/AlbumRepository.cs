using System.Linq;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        AlbumStoreDbContext db;
        public AlbumRepository(AlbumStoreDbContext db)
        {
            this.db = db;
        }

        public void Create(Album album)
        {
            db.Albums.Add(album);
            db.SaveChanges();
        }

        public Album Read(int id)
        {
            return db.Albums.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Album> ReadAll()
        {
            return db.Albums;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Album album)
        {
            var oldAlbum = Read(album.Id);
            oldAlbum.Id = album.Id;
            oldAlbum.Title = album.Title;
            oldAlbum.Stock = album.Stock;
            db.SaveChanges();
        }
    }
}
