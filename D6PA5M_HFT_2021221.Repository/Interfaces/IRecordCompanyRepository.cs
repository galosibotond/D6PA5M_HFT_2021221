using System.Linq;
using D6PA5M_HFT_2021221.Models;

namespace D6PA5M_HFT_2021221.Repository.Interfaces
{
    public interface IRecordCompanyRepository
    {
        void Create(RecordCompany recordCompany);
        void Delete(int id);
        RecordCompany Read(int id);
        IQueryable<RecordCompany> ReadAll();
        void Update(RecordCompany recordCompany);
    }
}