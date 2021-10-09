using System.Linq;
using D6PA5M_HFT_2021221.Data;
using D6PA5M_HFT_2021221.Models;
using D6PA5M_HFT_2021221.Repository.Interfaces;

namespace D6PA5M_HFT_2021221.Repository
{
    public class RecordCompanyRepository : IRecordCompanyRepository
    {
        AlbumStoreDbContext db;
        public RecordCompanyRepository(AlbumStoreDbContext db)
        {
            this.db = db;
        }

        public void Create(RecordCompany recordCompany)
        {
            db.RecordCompanies.Add(recordCompany);
            db.SaveChanges();
        }

        public RecordCompany Read(int id)
        {
            return db.RecordCompanies.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<RecordCompany> ReadAll()
        {
            return db.RecordCompanies;
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public void Update(RecordCompany recordCompany)
        {
            var oldRecordCompany = Read(recordCompany.Id);
            oldRecordCompany.Id = recordCompany.Id;
            oldRecordCompany.Name = recordCompany.Name;
            db.SaveChanges();
        }
    }
}
