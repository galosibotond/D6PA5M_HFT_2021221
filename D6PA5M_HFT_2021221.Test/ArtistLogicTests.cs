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
        private MockedRepositoryHelper mockedRepositoryHelper;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            mockedRepositoryHelper = new MockedRepositoryHelper();
        }

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

            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = 
                mockedRepositoryHelper.GetArtistRepository(artistsInRepository, artistToDoModificationWith: artistToCreate);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            artistLogic.CreateArtist(artistToCreate);

            // Assert
            Assert.That(artistsInRepository.Count(), Is.EqualTo(5));
            Assert.That(artistsInRepository.Contains(artistToCreate));
        }

        [Test]
        public void GivenArtistLogic_WhenCreateArtistWithoutName_ThenArgumentExceptionIsExpected()
        {
            // Arrange
            Artist artistToCreate = new Artist()
            {
                Id = 9999,
                Country = "UnknownCountry",
                FoundationDate = DateTime.Now,
                GenreId = 8888
            };

            IArtistLogic artistLogic = new ArtistLogic(Mock.Of<IArtistRepository>());

            // Action - Assert
            Assert.Throws<ArgumentException>(() => artistLogic.CreateArtist(artistToCreate));
        }

        [Test]
        public void GivenArtistLogic_WhenReadArtist_ThenArtistIsReadCorrectly()
        {
            // Arrange
            int artistIdToRead = 1111;

            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = 
                mockedRepositoryHelper.GetArtistRepository(artistsInRepository, artistIdToRead);
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
            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = mockedRepositoryHelper.GetArtistRepository(artistsInRepository); 
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

            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = 
                mockedRepositoryHelper.GetArtistRepository(artistsInRepository, artistIdToDelete);
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

            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = 
                mockedRepositoryHelper.GetArtistRepository(artistsInRepository, artistToDoModificationWith: artistToUpdate);
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
            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = mockedRepositoryHelper.GetArtistRepository(artistsInRepository);
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
            List<Artist> artistsInRepository = mockedRepositoryHelper.GetArtistsInRepository();
            IArtistRepository artistRepository = mockedRepositoryHelper.GetArtistRepository(artistsInRepository);
            IArtistLogic artistLogic = new ArtistLogic(artistRepository);

            // Action
            IEnumerable<KeyValuePair<string, int>> overallStockByArtists = artistLogic.GetOverallStockByArtists();

            // Assert
            Assert.That(overallStockByArtists.FirstOrDefault().Key, Is.EqualTo("FirstArtist"));
            Assert.That(overallStockByArtists.FirstOrDefault().Value, Is.EqualTo(8321));
        }
    }
}
