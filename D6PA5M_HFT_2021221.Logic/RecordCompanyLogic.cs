using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Logic
{
    public class RecordCompanyLogic : IRecordCompanyLogic
    {
        private readonly IRecordCompanyRepository recordCompanyRepository;

        public RecordCompanyLogic(IRecordCompanyRepository recordCompanyRepository)
        {
            this.recordCompanyRepository =
                recordCompanyRepository ?? throw new ArgumentNullException(nameof(recordCompanyRepository));
        }
        public void CreateRecordCompany(RecordCompany recordCompany)
        {
            this.recordCompanyRepository.Create(recordCompany);
        }

        public void DeleteRecordCompany(int id)
        {
            this.recordCompanyRepository.Delete(id);
        }

        public RecordCompany ReadRecordCompany(int id)
        {
            return recordCompanyRepository.Read(id);
        }

        public IEnumerable<RecordCompany> ReadAllRecordCompanies()
        {
            IEnumerable<RecordCompany> recordCompanies = recordCompanyRepository.ReadAll();

            return recordCompanies;
        }

        public void UpdateRecordCompany(RecordCompany recordCompany)
        {
            this.recordCompanyRepository.Update(recordCompany);
        }
    }
}
