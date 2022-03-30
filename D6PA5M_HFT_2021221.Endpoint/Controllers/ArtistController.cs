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
    public class ArtistController : ControllerBase
    {
        IArtistLogic artistLogic;
        IHubContext<SignalRHub> hub;

        public ArtistController(IArtistLogic artistLogic, IHubContext<SignalRHub> hub)
        {
            this.artistLogic = artistLogic;
            this.hub = hub;
        }

        // GET: /artist
        [HttpGet]
        public IEnumerable<Artist> Get()
        {
            return artistLogic.ReadAllArtists();
        }

        // GET /artist/1
        [HttpGet("{id}")]
        public Artist Get(int id)
        {
            return artistLogic.ReadArtist(id);
        }

        // POST /artist
        [HttpPost]
        public void Post([FromBody] Artist value)
        {
            artistLogic.CreateArtist(value);
            this.hub.Clients.All.SendAsync("ArtistCreated", value);
        }

        // PUT /artist
        [HttpPut]
        public void Put([FromBody] Artist value)
        {
            artistLogic.UpdateArtist(value);
            this.hub.Clients.All.SendAsync("ArtistUpdated", value);
        }

        // DELETE /artist/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Artist artistToDelete = artistLogic.ReadArtist(id);
            artistLogic.DeleteArtist(id);
            this.hub.Clients.All.SendAsync("ArtistDeleted", artistToDelete);
        }
    }
}
