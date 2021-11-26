using System.Collections.Generic;
using D6PA5M_HFT_2021221.Logic;
using D6PA5M_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        IGenreLogic genreLogic;

        public GenreController(IGenreLogic genreLogic)
        {
            this.genreLogic = genreLogic;
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
        }

        // PUT /genre
        [HttpPut]
        public void Put([FromBody] Genre value)
        {
            genreLogic.UpdateGenre(value);
        }

        // DELETE /genre/2
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            genreLogic.DeleteGenre(id);
        }
    }
}
