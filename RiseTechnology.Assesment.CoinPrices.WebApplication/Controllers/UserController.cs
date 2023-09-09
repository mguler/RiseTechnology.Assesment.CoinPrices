using Microsoft.AspNetCore.Mvc;
using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;
using RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement;
using Microsoft.AspNetCore.Authorization;

namespace RiseTechnology.Assesment.CoinPrices.WebApplication.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserManagementService _userManagementService;

        public UserController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _userManagementService.Logout();
            return Redirect("/");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login([FromBody]LoginDto loginDto)
        {
            var result = _userManagementService.Login(loginDto);
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