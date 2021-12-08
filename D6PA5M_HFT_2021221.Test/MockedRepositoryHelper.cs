using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;
using Moq;

namespace D6PA5M_HFT_2021221.Test
{
    public class MockedRepositoryHelper
    {

        public IAlbumRepository GetAlbumRepository(
                                                      List<Album> albumsInRepository,
                                                      int albumIdToDoModificationWith = 0,
                                                      Album albumToDoModificationWith = null)
        {
            Mock<IAlbumRepository> albumRepositoryMock = new Mock<IAlbumRepository>();

            albumRepositoryMock
                .Setup(albumRepository => albumRepository.ReadAll())
                .Returns(albumsInRepository.AsQueryable());

            if (albumToDoModificationWith != null)
            {
                albumRepositoryMock
                    .Setup(albumRepository => albumRepository.Create(albumToDoModificationWith))
                    .Callback(() => albumsInRepository.Add(albumToDoModificationWith));

                albumRepositoryMock
                    .Setup(albumRepository => albumRepository.Update(albumToDoModificationWith))
                    .Callback(() =>
                    {
                        int indexInList = albumsInRepository.FindIndex(artist => artist.Id == albumToDoModificationWith.Id);
                        albumsInRepository.RemoveAt(indexInList);
                        albumsInRepository.Add(albumToDoModificationWith);
                    });
            }

            if (albumIdToDoModificationWith != 0)
            {
                albumRepositoryMock
                    .Setup(albumRepository => albumRepository.Read(albumIdToDoModificationWith))
                    .Returns(albumsInRepository.Where(artist => artist.Id == albumIdToDoModificationWith).FirstOrDefault());

                albumRepositoryMock
                    .Setup(albumRepository => albumRepository.Delete(albumIdToDoModificationWith))
                    .Callback(() =>
                    {
                        int indexInList = albumsInRepository.FindIndex(artist => artist.Id == albumIdToDoModificationWith);
                        albumsInRepository.RemoveAt(indexInList);
                    });
            }

            return albumRepositoryMock.Object;
        }

        public List<Album> GetAlbumsInRepository()
        {
            Genre firstGenre = new Genre()
            {
                Name = "FirstGenre"
            };

            Genre secondGenre = new Genre()
            {
                Name = "SecondGenre"
            };

            Artist firstArtist = new Artist()
            {
                Name = "FirstArtist",
                GenreId = 1001,
                Country = "Hungary",
                Genre = firstGenre
            };

            Artist secondArtist = new Artist()
            {
                Name = "SecondArtist",
                GenreId = 1002,
                Country = "USA",
                Genre = secondGenre
            };

            RecordCompany firstRecordCompany = new RecordCompany()
            {
                Name = "FirstRecordCompany"
            };

            RecordCompany secondRecordCompany = new RecordCompany()
            {
                Name = "SecondRecordCompany"
            };

            RecordCompany thirdRecordCompany = new RecordCompany()
            {
                Name = "ThirdRecordCompany"
            };

            return new List<Album>
            {
                new Album()
                {
                    Id = 0001,
                    Title = "FirstAlbum",
                    Stock = 1000,
                    RecordCompanyId = 0101,
                    Artist = firstArtist,
                    Price = 1990,
                    RecordCompany = firstRecordCompany
                },

                new Album()
                {
                    Id = 0002,
                    Title = "SecondAlbum",
                    Stock = 150,
                    RecordCompanyId = 0101,
                    Artist = firstArtist,
                    Price = 2490,
                    RecordCompany = firstRecordCompany
                },

                new Album()
                {
                    Id = 0003,
                    Title = "ThirdAlbum",
                    Stock = 1000,
                    RecordCompanyId = 0102,
                    Artist = firstArtist,
                    Price = 4990,
                    RecordCompany = secondRecordCompany
                },

                new Album()
                {
                    Id = 0004,
                    Title = "FourthAlbum",
                    Stock = 3957,
                    RecordCompanyId = 0103,
                    Artist = secondArtist,
                    Price = 2990,
                    RecordCompany = thirdRecordCompany
                }
            };
        }
    }
}
