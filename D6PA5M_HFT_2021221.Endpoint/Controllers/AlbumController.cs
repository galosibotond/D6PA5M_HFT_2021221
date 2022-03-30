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
    public class AlbumController : ControllerBase
    {
        IAlbumLogic albumLogic;
        IHubContext<SignalRHub> hub;

        public AlbumController(IAlbumLogic albumLogic, IHubContext<SignalRHub> hub)
        {
            this.albumLogic = albumLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("AlbumCreated", value);
        }

        // PUT /album
        [HttpPut]
        public void Put([FromBody] Album value)
        {
            albumLogic.UpdateAlbum(value);
            this.hub.Clients.All.SendAsync("AlbumUpdated", value);
        }

        // DELETE /album/3
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Album albumToDelete = albumLogic.ReadAlbum(id);
            albumLogic.DeleteAlbum(id);
            this.hub.Clients.All.SendAsync("AlbumDeleted", albumToDelete);
        }
    }
}
