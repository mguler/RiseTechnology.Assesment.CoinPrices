using Microsoft.AspNetCore.Mvc;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;

namespace RiseTechnology.Assesment.DataApi.Controllers
{
    public class CoinManagementController : Controller
    {
        public CoinManagementController()
        {
        }

        [HttpGet]
        public JsonResult GetPrices(PriceInfoFilter priceInfoFilter) 
        {
            throw new NotImplementedException();
        }
    }
}