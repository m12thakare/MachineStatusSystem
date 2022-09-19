using MachineStatusSystem.Dto;
using MachineStatusSystem.Models;
using MachineStatusSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MachineStatusSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _Db;
        private readonly IAuthRepository _authRepo;
        public HomeController(ILogger<HomeController> logger, IAuthRepository authRepo,UserContext userContext)
        {
            _logger = logger;
            _authRepo = authRepo;
            _Db = userContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("Home/Register")]
        public IActionResult Register()
        {
            if(!ModelState.IsValid)
            {
                return View("Register");
            }
          
            return View();
        }


        [HttpPost("Home/Register")]
        public ActionResult<int> Register(UserRegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }
            else {
              
                var user = new LoginUser { Username = request.UserName };
                var response = _authRepo.Register(user, request.Password);
               
                return View("Login");
            }
           
        }


      /*  [Route("Home/Login")]*/
        public IActionResult Login()
        
        {
           return View();
        }
        public IActionResult Logout()

        {
            HttpContext.Session.Clear();
           return View("Login");
        }

        [HttpPost("Home/Login")]
        public ActionResult<string> Login(UserRegisterDto request)
        {
           
            var response = _authRepo.Login(request.UserName, request.Password);
            if(response=="0")
            {
                ViewBag.msg = "User Not Exsist plz register";
                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Register",
                  });
            }
           else
            {
              
                HttpContext.Session.SetString("UserId", response);
                var userList = _Db.User.ToList();

                return RedirectToRoute(new
                {
                    controller = "User",
                    action = "UserList",
                    UserList = userList
                });
            }
          
        }
    }
}
