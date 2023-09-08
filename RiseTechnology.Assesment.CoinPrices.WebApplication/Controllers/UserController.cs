using Microsoft.AspNetCore.Mvc;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Business.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.WebApplication.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManagementService _userManagementService;

        public UserController(UserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
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
            var result = new ServiceResultDto();
            return Json(result);
        }

        [HttpPost]
        public JsonResult Register([FromBody] RegisterDto registerDto)
        {
            var result = _userManagementService.Register(registerDto);
            return Json(result);
        }
    }
}