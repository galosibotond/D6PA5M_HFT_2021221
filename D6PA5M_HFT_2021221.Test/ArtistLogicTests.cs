using System;
using System.Collections.Generic;
using System.Linq;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace D6PA5M_HFT_2021221.Test
{
    [TestFixture]
    public class ArtistLogicTests
    {
        [Test]
        public void GivenNullArtistRepositoryMock_WhenInstantiateArtistLogic_ExceptionIsExpected()
        {
            Assert.Throws<ArgumentNullException>(() => new ArtistLogic(null));
        }

        [Test]
        public void GivenArtistLogic_WhenCreateArtist_ThenArtistIsCreatedSuccessfullyInRepository()
        {
            // Arrange
            Artist artistToCreate = new Artist()
            {
                Id = 9999,
                Name = "ArtistToCreate",
                Country = "UnknownCountry",
                FoundationDate = DateTime.Now,
                GenreId = 8888
            };

            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository, artistToDoModificationWith: artistToCreate);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            artistLogic.CreateArtist(artistToCreate);

            // Assert
            Assert.That(artistsInRepository.Count(), Is.EqualTo(5));
            Assert.That(artistsInRepository.Contains(artistToCreate));
        }

        [Test]
        public void GivenArtistLogic_WhenReadArtist_ThenArtistIsReadCorrectly()
        {
            // Arrange
            int artistIdToRead = 1111;

            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository, artistIdToRead);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            Artist readArtist = artistLogic.ReadArtist(artistIdToRead);

            // Assert
            Assert.That(readArtist, Is.Not.Null);
            Assert.That(readArtist.Id, Is.EqualTo(artistIdToRead));
        }

        [Test]
        public void GivenArtistLogic_WhenReadAllArtists_ThenArtistsAreReadCorrectly()
        {
            // Arrange
            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository); 
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            IEnumerable<Artist> allArtists = artistLogic.ReadAllArtists();

            // Assert
            Assert.That(allArtists.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GivenArtistLogic_WhenDeleteArtist_ThenArtistGetsDeletedFromTheRepository()
        {
            // Arrange
            int artistIdToDelete = 1111;

            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository, artistIdToDelete);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            artistLogic.DeleteArtist(artistIdToDelete);

            // Assert
            Assert.That(artistsInRepository.Where(artist => artist.Id == artistIdToDelete).Count(), Is.EqualTo(0));
        }

        [Test]
        public void GivenArtistLogic_WhenUpdateArtist_ThenArtistIsUpdatedSuccessfullyInRepository()
        {
            // Arrange
            Artist artistToUpdate = new Artist()
            {
                Id = 1111,
                Name = "UpdatedArtistName",
                Country = "UnknownCountry",
                FoundationDate = DateTime.Now,
                GenreId = 6666
            };

            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository, artistToDoModificationWith: artistToUpdate);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            artistLogic.UpdateArtist(artistToUpdate);

            // Assert
            Assert.That(
                artistsInRepository.Where(artist => artist.Id == artistToUpdate.Id).FirstOrDefault().Name, Is.EqualTo(artistToUpdate.Name));

            Assert.That(
                artistsInRepository.Where(artist => artist.Id == artistToUpdate.Id).FirstOrDefault().GenreId, Is.EqualTo(artistToUpdate.GenreId));
        }

        [Test]
        public void GivenArtistLogic_WhenGetMostUnselledAlbumByArtists_ThenAlbumNameIsCorrectForArtist()
        {
            // Arrange
            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            IEnumerable<KeyValuePair<string, string>> mostUnselledAlbumByArtists = artistLogic.GetMostUnselledAlbumByArtists();

            // Assert
            Assert.That(mostUnselledAlbumByArtists.FirstOrDefault().Key, Is.EqualTo("FirstArtist"));
            Assert.That(mostUnselledAlbumByArtists.FirstOrDefault().Value, Is.EqualTo("FourthAlbum"));
        }

        [Test]
        public void GivenArtistLogic_WhenGetOverallStockByArtists_ThenAlbumNameIsCorrectForArtist()
        {
            // Arrange
            List<Artist> artistsInRepository = GetArtistsInRepository();
            IArtistRepository artistRepository = GetArtistRepository(artistsInRepository);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            IEnumerable<KeyValuePair<string, int>> overallStockByArtists = artistLogic.GetOverallStockByArtists();

            // Assert
            Assert.That(overallStockByArtists.FirstOrDefault().Key, Is.EqualTo("FirstArtist"));
            Assert.That(overallStockByArtists.FirstOrDefault().Value, Is.EqualTo(8321));
        }

        private IArtistRepository GetArtistRepository(
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
    }
}
