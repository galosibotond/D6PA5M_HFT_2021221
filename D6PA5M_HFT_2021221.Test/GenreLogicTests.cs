using System;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace D6PA5M_HFT_2021221.Test
{
    [TestFixture]
    public class GenreLogicTests
    {

        [Test]
        public void GivenNullGenreRepository_WhenInstantiateGenreLogic_ExceptionIsExpected()
        {
            Assert.Throws<ArgumentNullException>(() => new GenreLogic(null));
        }

        [Test]
        public void GivenGenreRepository_WhenCreateGenreWithoutName_ThenArgumentExceptionIsExpected()
        {
            // Arrange
            Genre genreToCreate = new Genre()
            {
                Id = 0000
            };

            IGenreLogic genreLogic = new GenreLogic(Mock.Of<IGenreRepository>());

            // Action - Assert
            Assert.Throws<ArgumentException>(() => genreLogic.CreateGenre(genreToCreate));
        }
    }
}
