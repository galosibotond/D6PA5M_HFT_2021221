using System.Linq;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Repository.Interfaces
{
    public interface IGenreRepository
    {
        void Create(Genre genre);
        void Delete(int id);
        Genre Read(int id);
        IQueryable<Genre> ReadAll();
        void Update(Genre genre);
    }
}