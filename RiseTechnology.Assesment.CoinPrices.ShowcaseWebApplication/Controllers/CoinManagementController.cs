using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RiseTechnology.Assesment.CoinPrices.WebApplication.Controllers
{
    public class CoinManagementController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Showcase()
        {
            return View();
        }
    }
}