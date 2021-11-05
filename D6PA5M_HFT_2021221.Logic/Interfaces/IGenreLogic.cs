using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Logic
{
    public interface IGenreLogic
    {
        void CreateGenre(Genre genre);
        void DeleteGenre(int id);
        IEnumerable<Genre> ReadAllGenres();
        Genre ReadGenre(int id);
        void UpdateGenre(Genre genre);
    }
}