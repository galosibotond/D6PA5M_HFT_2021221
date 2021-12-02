using System.Collections.Generic;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordCompanyController : ControllerBase
    {
        IRecordCompanyLogic recordCompanyLogic;

        public RecordCompanyController(IRecordCompanyLogic recordCompanyLogic)
        {
            this.recordCompanyLogic = recordCompanyLogic;
        }

        // GET: /recordcompany
        [HttpGet]
        public IEnumerable<RecordCompany> Get()
        {
            return recordCompanyLogic.ReadAllRecordCompanies();
        }

        // GET /recordcompany/2
        [HttpGet("{id}")]
        public RecordCompany Get(int id)
        {
            return recordCompanyLogic.ReadRecordCompany(id);
        }

        // POST /recordcompany
        [HttpPost]
        public void Post([FromBody] RecordCompany value)
        {
            recordCompanyLogic.CreateRecordCompany(value);
        }

        // PUT /recordcompany
        [HttpPut]
        public void Put([FromBody] RecordCompany value)
        {
            recordCompanyLogic.UpdateRecordCompany(value);
        }

        // DELETE /recordcompany/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            recordCompanyLogic.DeleteRecordCompany(id);
        }
    }
}
