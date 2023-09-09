using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.CoinManagement;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;

namespace RiseTechnology.Assesment.DataApi.Controllers
{
    public class CoinManagementController : Controller
    {

        private readonly ICoinManagementService _coinManagementService;

        public CoinManagementController(ICoinManagementService coinManagementService)
        {
            _coinManagementService = coinManagementService;
        }
        [Authorize]
        public JsonResult GetPrices(PriceInfoFilter priceInfoFilter)
        {
            var result = _coinManagementService.GetPriceInfo(priceInfoFilter);
            return Json(result);
        }
    }
}