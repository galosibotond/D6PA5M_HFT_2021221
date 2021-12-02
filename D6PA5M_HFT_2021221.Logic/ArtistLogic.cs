using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Logic
{
    public class ArtistLogic : IArtistLogic
    {
        private readonly IArtistRepository artistRepository;

        public ArtistLogic(IArtistRepository artistRepository)
        {
            this.artistRepository =
                artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
        }
        public void CreateArtist(Artist artist)
        {
            if (string.IsNullOrEmpty(artist.Name))
            {
                throw new ArgumentException(nameof(artist));
            }

            this.artistRepository.Create(artist);
        }

        public void DeleteArtist(int id)
        {
            this.artistRepository.Delete(id);
        }

        public Artist ReadArtist(int id)
        {
            return artistRepository.Read(id);
        }

        public IEnumerable<Artist> ReadAllArtists()
        {
            IEnumerable<Artist> artists = artistRepository.ReadAll();

            return artists;
        }

        public void UpdateArtist(Artist artist)
        {
            this.artistRepository.Update(artist);
        }

        public IEnumerable<KeyValuePair<string, int>> GetOverallStockByArtists()
        {
            var overallStockByArtistsQuery = from artist in ReadAllArtists()
                                             group artist by artist.Name into g
                                             orderby g.Key
                                             select new KeyValuePair<string, int>(
                                                                                  g.Key,
                                                                                  g.Select(a => a.Albums.Sum(alb => alb.Stock)).FirstOrDefault());

            return overallStockByArtistsQuery.ToList();
        }

        public IEnumerable<KeyValuePair<string, string>> GetMostUnselledAlbumByArtists()
        {
            var mostUnselledAlbumByArtistsQuery = from artist in ReadAllArtists()
                                                  group artist by artist.Name into g
                                                  orderby g.Key
                                                  select new KeyValuePair<string, string>(
                                                      g.Key,
                                                      g.Select(art => art.Albums.OrderByDescending(alb => alb.Stock)
                                                               .Select(alb => alb.Title))
                                                      .FirstOrDefault()
                                                      .FirstOrDefault());

            return mostUnselledAlbumByArtistsQuery.ToList();
        }
    }
}
