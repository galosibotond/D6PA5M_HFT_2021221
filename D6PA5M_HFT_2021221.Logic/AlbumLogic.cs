using System;
using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Logic
{
    public class AlbumLogic : IAlbumLogic
    {
        private readonly IAlbumRepository albumRepository;

        public AlbumLogic(IAlbumRepository albumRepository)
        {
            this.albumRepository =
                albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
        }
        public void CreateAlbum(Album album)
        {
            this.albumRepository.Create(album);
        }

        public void DeleteAlbum(int id)
        {
            this.albumRepository.Delete(id);
        }

        public Album ReadAlbum(int id)
        {
            return albumRepository.Read(id);
        }

        public IEnumerable<Album> ReadAllAlbums()
        {
            IEnumerable<Album> albums = albumRepository.ReadAll();

            return albums;
        }

        public void UpdateAlbum(Album album)
        {
            this.albumRepository.Update(album);
        }
    }
}
