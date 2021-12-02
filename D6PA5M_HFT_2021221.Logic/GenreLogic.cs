using System;
using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Logic
{
    public class GenreLogic : IGenreLogic
    {
        private readonly IGenreRepository genreRepository;

        public GenreLogic(IGenreRepository genreRepository)
        {
            this.genreRepository =
                genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        }
        public void CreateGenre(Genre genre)
        {
            if (string.IsNullOrEmpty(genre.Name))
            {
                throw new ArgumentException(nameof(genre));
            }

            this.genreRepository.Create(genre);
        }

        public void DeleteGenre(int id)
        {
            this.genreRepository.Delete(id);
        }

        public Genre ReadGenre(int id)
        {
            return genreRepository.Read(id);
        }

        public IEnumerable<Genre> ReadAllGenres()
        {
            IEnumerable<Genre> genres = genreRepository.ReadAll();

            return genres;
        }

        public void UpdateGenre(Genre genre)
        {
            this.genreRepository.Update(genre);
        }
    }
}
