using System;
using D6PA5M_HFT_2021221.Logic;
using NUnit.Framework;

namespace D6PA5M_HFT_2021221.Test
{
    [TestFixture]
    public class RecordCompanyLogicTests
    {

        [Test]
        public void GivenNullRecordCompanyRepository_WhenInstantiateRecordCompanyLogic_ExceptionIsExpected()
        {
            Assert.Throws<ArgumentNullException>(() => new RecordCompanyLogic(null));
        }
    }
}
