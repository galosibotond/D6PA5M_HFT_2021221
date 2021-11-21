using System.Collections;
using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Logic
{
    public interface IArtistLogic
    {
        void CreateArtist(Artist artist);
        void DeleteArtist(int id);
        IEnumerable<Artist> ReadAllArtists();
        Artist ReadArtist(int id);
        void UpdateArtist(Artist artist);
        IEnumerable GetOverallStockByArtists();
        IEnumerable GetMostUnselledAlbumByArtists();
    }
}