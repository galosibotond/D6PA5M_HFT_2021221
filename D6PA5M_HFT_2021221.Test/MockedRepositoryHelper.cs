using System;
using System.Collections.Generic;
using System.Linq;
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

        public IArtistRepository GetArtistRepository(
                                              List<Artist> artistsInRepository,
                                              int artistIdToDoModificationWith = 0,
                                              Artist artistToDoModificationWith = null)
        {
            Mock<IArtistRepository> artistRepositoryMock = new Mock<IArtistRepository>();

            artistRepositoryMock
                .Setup(artistRepository => artistRepository.ReadAll())
                .Returns(artistsInRepository.AsQueryable());

            if (artistToDoModificationWith != null)
            {
                artistRepositoryMock
                    .Setup(artistRepository => artistRepository.Create(artistToDoModificationWith))
                    .Callback(() => artistsInRepository.Add(artistToDoModificationWith));

                artistRepositoryMock
                    .Setup(artistRepository => artistRepository.Update(artistToDoModificationWith))
                    .Callback(() =>
                    {
                        int indexInList = artistsInRepository.FindIndex(artist => artist.Id == artistToDoModificationWith.Id);
                        artistsInRepository.RemoveAt(indexInList);
                        artistsInRepository.Add(artistToDoModificationWith);
                    });
            }

            if (artistIdToDoModificationWith != 0)
            {
                artistRepositoryMock
                    .Setup(artistRepository => artistRepository.Read(artistIdToDoModificationWith))
                    .Returns(artistsInRepository.Where(artist => artist.Id == artistIdToDoModificationWith).FirstOrDefault());

                artistRepositoryMock
                    .Setup(artistRepository => artistRepository.Delete(artistIdToDoModificationWith))
                    .Callback(() =>
                    {
                        int indexInList = artistsInRepository.FindIndex(artist => artist.Id == artistIdToDoModificationWith);
                        artistsInRepository.RemoveAt(indexInList);
                    });
            }

            return artistRepositoryMock.Object;
        }

        public List<Artist> GetArtistsInRepository()
        {
            List<Album> firstArtistAlbums = new List<Album>
            {
                new Album()
                {
                    Id = 0001,
                    Title = "FirstAlbum",
                    Stock = 1000,
                    RecordCompanyId = 0101,
                    Price = 1990,
                },

                new Album()
                {
                    Id = 0002,
                    Title = "SecondAlbum",
                    Stock = 150,
                    RecordCompanyId = 0101,
                    Price = 2490,
                },

                new Album()
                {
                    Id = 0003,
                    Title = "ThirdAlbum",
                    Stock = 3214,
                    RecordCompanyId = 0102,
                    Price = 4990,
                },

                new Album()
                {
                    Id = 0004,
                    Title = "FourthAlbum",
                    Stock = 3957,
                    RecordCompanyId = 0103,
                    Price = 2990,
                }
            };

            List<Album> secondArtistAlbums = new List<Album>
            {
                new Album()
                {
                    Id = 0005,
                    Title = "FirstAlbum",
                    Stock = 100,
                    RecordCompanyId = 0101,
                    Price = 1990,
                }
            };

            List<Album> thirdArtistAlbums = new List<Album>
            {
                new Album()
                {
                    Id = 0006,
                    Title = "FirstAlbum",
                    Stock = 1000,
                    RecordCompanyId = 0101,
                    Price = 1990,
                },

                new Album()
                {
                    Id = 0007,
                    Title = "SecondAlbum",
                    Stock = 879,
                    RecordCompanyId = 0101,
                    Price = 2490,
                }
            };

            List<Album> fourthArtistAlbums = new List<Album>
            {
                new Album()
                {
                    Id = 0008,
                    Title = "FirstAlbum",
                    Stock = 154,
                    RecordCompanyId = 0101,
                    Price = 1990,
                },

                new Album()
                {
                    Id = 0009,
                    Title = "SecondAlbum",
                    Stock = 31,
                    RecordCompanyId = 0101,
                    Price = 2490,
                },

                new Album()
                {
                    Id = 0010,
                    Title = "ThirdAlbum",
                    Stock = 10,
                    RecordCompanyId = 0102,
                    Price = 4990,
                },
            };

            return new List<Artist>
            {
                new Artist()
                {
                    Id = 1111,
                    Name = "FirstArtist",
                    Country = "UnknownCountry",
                    FoundationDate = DateTime.Now,
                    GenreId = 0101,
                    Albums = firstArtistAlbums
                },

                new Artist()
                {
                    Id = 2222,
                    Name = "SecondArtist",
                    Country = "UnknownCountry",
                    FoundationDate = DateTime.Now,
                    GenreId = 0102,
                    Albums = secondArtistAlbums
                },

                new Artist()
                {
                    Id = 3333,
                    Name = "ThirdArtist",
                    Country = "UnknownCountry",
                    FoundationDate = DateTime.Now,
                    GenreId = 0103,
                    Albums = thirdArtistAlbums
                },

                new Artist()
                {
                    Id = 4444,
                    Name = "FourthArtist",
                    Country = "UnknownCountry",
                    FoundationDate = DateTime.Now,
                    GenreId = 0104,
                    Albums = fourthArtistAlbums
                }
            };
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
