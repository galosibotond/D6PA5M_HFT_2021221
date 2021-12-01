using System.Collections;
using System.Collections.Generic;
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
        public IEnumerable<KeyValuePair<string, int>> StockByArtist()
        {
            return artistLogic.GetOverallStockByArtists();
        }

        // GET: stat/mostunselledalbum
        [HttpGet("mostunselledalbum")]
        public IEnumerable<KeyValuePair<string, string>> MostUnselledAlbum()
        {
            return artistLogic.GetMostUnselledAlbumByArtists();
        }

        // GET: stat/albumsbycountry
        [HttpGet("albumscountbycountry")]
        public IEnumerable<KeyValuePair<string, int>> AlbumsCountByCountry()
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
        public IEnumerable<KeyValuePair<string, double>> AVGAlbumPriceByGenre()
        {
            return albumLogic.GetAverageAlbumPriceByGenres();
        }

        // GET: stat/avgalbumpricebyrecordcompany
        [HttpGet("avgalbumpricebyrecordcompany")]
        public IEnumerable<KeyValuePair<string, double>> AVGAlbumPriceByRecordCompany()
        {
            return albumLogic.GetAverageAlbumPriceByRecordCompanies();
        }
    }
}
