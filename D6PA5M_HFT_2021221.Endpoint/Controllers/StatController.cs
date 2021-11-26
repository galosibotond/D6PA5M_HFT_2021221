using System.Collections;
using D6PA5M_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;

namespace D6PA5M_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IArtistLogic artistLogic;
        IAlbumLogic albumLogic;

        public StatController(IArtistLogic artistLogic, IAlbumLogic albumLogic)
        {
            this.artistLogic = artistLogic;
            this.albumLogic = albumLogic;
        }


        // GET: stat/stockbyartist
        [HttpGet("stockbyartist")]
        public IEnumerable StockByArtist()
        {
            return artistLogic.GetOverallStockByArtists();
        }

        // GET: stat/mostunselledalbum
        [HttpGet("mostunselledalbum")]
        public IEnumerable MostUnselledAlbum()
        {
            return artistLogic.GetMostUnselledAlbumByArtists();
        }

        // GET: stat/albumsbycountry
        [HttpGet("albumsbycountry")]
        public IEnumerable AlbumsByCountry()
        {
            return albumLogic.GetAlbumsCountByCountry();
        }

        // GET: stat/avgalbumprice
        [HttpGet("avgalbumprice")]
        public double AVGAlbumPrice()
        {
            return albumLogic.GetAverageAlbumPrice();
        }

        // GET: stat/avgalbumpricebygenre
        [HttpGet("avgalbumpricebygenre")]
        public IEnumerable AVGAlbumPriceByGenre()
        {
            return albumLogic.GetAverageAlbumPriceByGenres();
        }

        // GET: stat/avgalbumpricebyrecordcompany
        [HttpGet("avgalbumpricebyrecordcompany")]
        public IEnumerable AVGAlbumPriceByRecordCompany()
        {
            return albumLogic.GetAverageAlbumPriceByRecordCompanies();
        }
    }
}
