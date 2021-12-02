using System;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;
using Moq;
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

        [Test]
        public void GivenRecordCompanyRepository_WhenCreateRecordCompanyWithoutName_ThenArgumentExceptionIsExpected()
        {
            // Arrange
            RecordCompany recordCompanyToCreate = new RecordCompany()
            {
                Id = 0000
            };

            IRecordCompanyLogic recordCompanyLogic = new RecordCompanyLogic(Mock.Of<IRecordCompanyRepository>());

            // Action - Assert
            Assert.Throws<ArgumentException>(() => recordCompanyLogic.CreateRecordCompany(recordCompanyToCreate));
        }
    }
}
