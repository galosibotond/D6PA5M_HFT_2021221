using System.Collections.Generic;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Logic
{
    public interface IRecordCompanyLogic
    {
        void CreateRecordCompany(RecordCompany recordCompany);
        void DeleteRecordCompany(int id);
        IEnumerable<RecordCompany> ReadAllRecordCompanies();
        RecordCompany ReadRecordCompany(int id);
        void UpdateRecordCompany(RecordCompany recordCompany);
    }
}