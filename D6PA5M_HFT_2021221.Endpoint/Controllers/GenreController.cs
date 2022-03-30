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
    public class GenreController : ControllerBase
    {
        IGenreLogic genreLogic;
        IHubContext<SignalRHub> hub;

        public GenreController(IGenreLogic genreLogic, IHubContext<SignalRHub> hub)
        {
            this.genreLogic = genreLogic;
            this.hub = hub;
        }

        // GET: /genre
        [HttpGet]
        public IEnumerable<Genre> Get()
        {
            return genreLogic.ReadAllGenres();
        }

        // GET /genre/1
        [HttpGet("{id}")]
        public Genre Get(int id)
        {
            return genreLogic.ReadGenre(id);
        }

        // POST /genre
        [HttpPost]
        public void Post([FromBody] Genre value)
        {
            genreLogic.CreateGenre(value);
            this.hub.Clients.All.SendAsync("GenreCreated", value);
        }

        // PUT /genre
        [HttpPut]
        public void Put([FromBody] Genre value)
        {
            genreLogic.UpdateGenre(value);
            this.hub.Clients.All.SendAsync("GenreUpdated", value);
        }

        // DELETE /genre/2
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Genre genreToDelete = genreLogic.ReadGenre(id);
            genreLogic.DeleteGenre(id);
            this.hub.Clients.All.SendAsync("GenreDeleted", genreToDelete);
        }
    }
}
