using Microsoft.AspNetCore.Mvc;
namespace RiseTechnology.Assesment.CoinPrices.WebApplication.Controllers
{
    public class CoinManagementController : Controller
    {

        public CoinManagementController()
        {
        }

        [HttpGet]
        public IActionResult Showcase()
        {
            return View();
        }
    }
}