using Microsoft.AspNetCore.Mvc;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.WebApplication.Controllers
{
    public class UserController : Controller
    {
        public UserController()
        {
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginDto loginDto)
        {
            var result = new ServiceResponseDto();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register(RegisterDto registerDto)
        {
            var result = new ServiceResponseDto();
            return Json(result);
        }
    }
}