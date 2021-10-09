using System.Linq;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Repository.Interfaces
{
    public interface IAlbumRepository
    {
        void Create(Album album);
        void Delete(int id);
        Album Read(int id);
        IQueryable<Album> ReadAll();
        void Update(Album album);
    }
}