using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Logic
{
    public interface IAlbumLogic
    {
        void CreateAlbum(Album album);
        void DeleteAlbum(int id);
        Album ReadAlbum(int id);
        IEnumerable<Album> ReadAllAlbums();
        void UpdateAlbum(Album album);
    }
}