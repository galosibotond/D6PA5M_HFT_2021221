using System.Collections.Generic;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        IAlbumLogic albumLogic;

        public AlbumController(IAlbumLogic albumLogic)
        {
            this.albumLogic = albumLogic;
        }

        // GET: /album
        [HttpGet]
        public IEnumerable<Album> Get()
        {
            return albumLogic.ReadAllAlbums();
        }

        // GET /album/1
        [HttpGet("{id}")]
        public Album Get(int id)
        {
            return albumLogic.ReadAlbum(id);
        }

        // POST /album
        [HttpPost]
        public void Post([FromBody] Album value)
        {
            albumLogic.CreateAlbum(value);
        }

        // PUT /album
        [HttpPut]
        public void Put([FromBody] Album value)
        {
            albumLogic.UpdateAlbum(value);
        }

        // DELETE /album/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            albumLogic.DeleteAlbum(id);
        }
    }
}
