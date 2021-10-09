using System.Linq;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Repository.Interfaces
{
    public interface IArtistRepository
    {
        void Create(Artist artist);
        void Delete(int id);
        Artist Read(int id);
        IQueryable<Artist> ReadAll();
        void Update(Artist artist);
    }
}