using System;
using System.Collections;
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
    public class AlbumLogicTests
    {
        private MockedRepositoryHelper mockedRepositoryHelper;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            mockedRepositoryHelper = new MockedRepositoryHelper();
        }


        [Test]
        public void GivenNullAlbumRepository_WhenInstantiateAlbumLogic_ExceptionIsExpected()
        {
            Assert.Throws<ArgumentNullException>(() => new AlbumLogic(null));
        }

        [Test]
        public void GivenAlbumLogic_WhenCreateAlbum_ThenAlbumIsCreatedSuccessfullyInRepository()
        {
            // Arrange
            Album albumToCreate = new Album()
            {
                Id = 9999,
                Title = "AlbumToCreate",
                Stock = 1000,
                RecordCompanyId = 0101,
                ArtistId = 1111,
                Price = 1990
            };

            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = 
                mockedRepositoryHelper.GetAlbumRepository(albumsInRepository, albumToDoModificationWith: albumToCreate);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            albumLogic.CreateAlbum(albumToCreate);

            // Assert
            Assert.That(albumsInRepository.Count(), Is.EqualTo(5));
            Assert.That(albumsInRepository.Contains(albumToCreate));
        }

        [Test]
        public void GivenAlbumLogic_WhenCreateAlbumWithNoTitle_ThenArgumentExceptionIsExpected()
        {
            // Arrange
            Album albumToCreate = new Album()
            {
                Id = 9999,
                Stock = 1000,
                RecordCompanyId = 0101,
                ArtistId = 1111,
                Price = 1990
            };

            IAlbumLogic albumLogic = new AlbumLogic(Mock.Of<IAlbumRepository>());

            // Action - Assert
            Assert.Throws<ArgumentException>(() => albumLogic.CreateAlbum(albumToCreate));
        }

        [Test]
        public void GivenAlbumLogic_WhenReadAlbum_ThenAlbumIsReadCorrectly()
        {
            // Arrange
            int albumIdToRead = 0003;

            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository, albumIdToRead);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            Album readAlbum = albumLogic.ReadAlbum(albumIdToRead);

            // Assert
            Assert.That(readAlbum, Is.Not.Null);
            Assert.That(readAlbum.Id, Is.EqualTo(albumIdToRead));
        }

        [Test]
        public void GivenAlbumLogic_WhenReadAllAlbums_ThenAlbumsAreReadCorrectly()
        {
            // Arrange
            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            IEnumerable<Album> allAlbums = albumLogic.ReadAllAlbums();

            // Assert
            Assert.That(allAlbums.Count(), Is.EqualTo(4));
        }

        [Test]
        public void GivenAlbumLogic_WhenDeleteAlbum_ThenAlbumGetsDeletedFromTheRepository()
        {
            // Arrange
            int albumIdToDelete = 0002;

            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository, albumIdToDelete);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            albumLogic.DeleteAlbum(albumIdToDelete);

            // Assert
            Assert.That(albumsInRepository.Where(artist => artist.Id == albumIdToDelete).Count(), Is.EqualTo(0));
        }

        [Test]
        public void GivenAlbumLogic_WhenUpdateAlbum_ThenAlbumIsUpdatedSuccessfullyInRepository()
        {
            // Arrange
            Album albumToUpdate = new Album()
            {
                Id = 0001,
                Title = "FirstAlbumUpdated",
                Stock = 1348,
                RecordCompanyId = 0101,
                ArtistId = 1111,
                Price = 1990
            };

            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = 
                mockedRepositoryHelper.GetAlbumRepository(albumsInRepository, albumToDoModificationWith: albumToUpdate);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            albumLogic.UpdateAlbum(albumToUpdate);

            // Assert
            Assert.That(
                albumsInRepository.Where(artist => artist.Id == albumToUpdate.Id).FirstOrDefault().Title, Is.EqualTo(albumToUpdate.Title));

            Assert.That(
                albumsInRepository.Where(artist => artist.Id == albumToUpdate.Id).FirstOrDefault().Stock, Is.EqualTo(albumToUpdate.Stock));
        }

        [Test]
        public void GivenAlbumLogic_WhenGetAlbumsCountByCountry_ThenCorrectValueReturnsToKey()
        {
            // Arrange
            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            IEnumerable<KeyValuePair<string, int>> albumsCountByCountry = albumLogic.GetAlbumsCountByCountry();

            // Assert
            Assert.That(albumsCountByCountry.FirstOrDefault().Key, Is.EqualTo("Hungary"));
            Assert.That(albumsCountByCountry.FirstOrDefault().Value, Is.EqualTo(3));
        }

        [Test]
        public void GivenAlbumLogic_WhenGetAverageAlbumPrice_ThenAverageAlbumPriceIsCorrect()
        {
            // Arrange
            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            double averageAlbumPrice = albumLogic.GetAverageAlbumPrice();

            // Assert
            Assert.That(averageAlbumPrice, Is.EqualTo(3115));
        }

        [Test]
        public void GivenAlbumLogic_WhenGetAverageAlbumPriceByGenres_ThenAverageAlbumPriceIsCorrectForKey()
        {
            // Arrange
            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            IEnumerable<KeyValuePair<string, double>> averageAlbumPriceByGenre = albumLogic.GetAverageAlbumPriceByGenres();

            // Assert
            Assert.That(averageAlbumPriceByGenre.FirstOrDefault().Key, Is.EqualTo("FirstGenre"));
            Assert.That(Math.Round(averageAlbumPriceByGenre.FirstOrDefault().Value, 0), Is.EqualTo(3157));
        }

        [Test]
        public void GivenAlbumLogic_WhenGetAverageAlbumPriceByRecordCompany_ThenAverageAlbumPriceIsCorrectForKey()
        {
            // Arrange
            List<Album> albumsInRepository = mockedRepositoryHelper.GetAlbumsInRepository();
            IAlbumRepository albumRepository = mockedRepositoryHelper.GetAlbumRepository(albumsInRepository);
            IAlbumLogic albumLogic = new AlbumLogic(albumRepository);

            // Action
            IEnumerable<KeyValuePair<string, double>> averageAlbumPriceByRecordCompany = 
                                                albumLogic.GetAverageAlbumPriceByRecordCompanies();

            // Assert
            Assert.That(averageAlbumPriceByRecordCompany.FirstOrDefault().Key, Is.EqualTo("FirstRecordCompany"));
            Assert.That(Math.Round(averageAlbumPriceByRecordCompany.FirstOrDefault().Value, 0), Is.EqualTo(2240));
        }
    }
}
