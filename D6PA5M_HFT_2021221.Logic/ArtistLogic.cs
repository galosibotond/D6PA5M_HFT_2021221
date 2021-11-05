using System;
using System.Collections.Generic;
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
    }
}
