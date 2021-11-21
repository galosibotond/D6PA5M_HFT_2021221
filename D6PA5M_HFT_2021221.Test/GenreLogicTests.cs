using System;
using D6PA5M_HFT_2021221.Logic;
using NUnit.Framework;

namespace D6PA5M_HFT_2021221.Test
{
    [TestFixture]
    public class GenreLogicTests
    {

        [Test]
        public void GivenNullGenreRepository_WhenInstantiateArtistLogic_ExceptionIsExpected()
        {
            Assert.Throws<ArgumentNullException>(() => new GenreLogic(null));
        }
    }
}
