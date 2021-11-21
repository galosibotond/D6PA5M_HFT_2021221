using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public double GetAverageAlbumPrice()
        {
            return albumRepository.ReadAll().Average(album => album.Price);
        }

        public IEnumerable GetAverageAlbumPriceByGenres()
        {
            var averageAlbumPriceByGenresQuery = from album in albumRepository.ReadAll()
                                                 group album by album.Artist.Genre.Name into g
                                                 orderby g.Key
                                                 select new
                                                 {
                                                     AlbumGenre = g.Key,
                                                     AlbumAvgPrice = g.Average(a => a.Price)
                                                 };

            return averageAlbumPriceByGenresQuery.ToList();
        }

        public IEnumerable GetAverageAlbumPriceByRecordCompanies()
        {
            var averageAlbumPriceByRecordCompaniesQuery = from album in albumRepository.ReadAll()
                                                          group album by album.RecordCompany.Name into g
                                                          orderby g.Key
                                                          select new
                                                          {
                                                            AlbumRecordCompany = g.Key,
                                                            AlbumAvgPrice = g.Average(a => a.Price)
                                                          };

            return averageAlbumPriceByRecordCompaniesQuery.ToList();
        }

        public IEnumerable GetAlbumsCountByCountry()
        {
            var albumsCountByCountryQuery = from album in albumRepository.ReadAll()
                                            group album by album.Artist.Country into g
                                            orderby g.Key
                                            select new
                                            {
                                                 ArtistCountry = g.Key,
                                                 AlbumsCount = g.Count()
                                            };

            return albumsCountByCountryQuery.ToList();
        }
    }
}
