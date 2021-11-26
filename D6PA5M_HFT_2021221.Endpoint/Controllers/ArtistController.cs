using System.Collections.Generic;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        IArtistLogic artistLogic;

        public ArtistController(IArtistLogic artistLogic)
        {
            this.artistLogic = artistLogic;
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
        }

        // PUT /artist
        [HttpPut]
        public void Put([FromBody] Artist value)
        {
            artistLogic.UpdateArtist(value);
        }

        // DELETE /artist/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            artistLogic.DeleteArtist(id);
        }
    }
}
