using System.Linq;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Repository
{
    public class GenreRepository : IGenreRepository
    {
        AlbumStoreDbContext db;
        public GenreRepository(AlbumStoreDbContext db)
        {
            this.db = db;
        }

        public void Create(Genre genre)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
        }

        public Genre Read(int id)
        {
            return db.Genres.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Genre> ReadAll()
        {
            return db.Genres;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(Genre genre)
        {
            var oldGenre = Read(genre.Id);
            oldGenre.Id = genre.Id;
            oldGenre.Name = genre.Name;
            db.SaveChanges();
        }
    }
}
