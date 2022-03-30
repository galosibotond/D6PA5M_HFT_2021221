using System.Collections.Generic;
using D6PA5M_HFT_2021221.Endpoint.Services;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordCompanyController : ControllerBase
    {
        IRecordCompanyLogic recordCompanyLogic;
        IHubContext<SignalRHub> hub;

        public RecordCompanyController(IRecordCompanyLogic recordCompanyLogic, IHubContext<SignalRHub> hub)
        {
            this.recordCompanyLogic = recordCompanyLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("RecordCompanyCreated", value);
        }

        // PUT /recordcompany
        [HttpPut]
        public void Put([FromBody] RecordCompany value)
        {
            recordCompanyLogic.UpdateRecordCompany(value);
            this.hub.Clients.All.SendAsync("RecordCompanyUpdated", value);
        }

        // DELETE /recordcompany/1
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RecordCompany recordCompanyToDelete = recordCompanyLogic.ReadRecordCompany(id);
            recordCompanyLogic.DeleteRecordCompany(id);
            this.hub.Clients.All.SendAsync("RecordCompanyDeleted", recordCompanyToDelete);
        }
    }
}
