using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Projeto.Iris.Models;
using Projeto.Iris.ViewModels;
using Projeto.Iris.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projeto.Iris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, 
                              IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return IsLoginView("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return IsLoginView("Register");
        }

        [HttpPost]
        public IActionResult Register(User user)
        {

            var findUser = _userRepository.FindByEmail(user.Email);
            if (findUser == null)
            {
                //Encripitando a a sennha
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = passwordHash;

                _userRepository.Insert(user);
                _userRepository.Save();

                SetSession(user);

                return RedirectToAction("Index", "Photo");
            }
            else
            {
                TempData["error-msg"] = "Email já cadastrado!";
                return View();
            }
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


        //Session
        public void SetSession(User user)
        {
            HttpContext.Session.SetString("userId", user.UserId.ToString());
            HttpContext.Session.SetString("userName", user.Name);
            ViewBag.userId = user.UserId.ToString();
            ViewBag.userName = user.Name;
        }

        public void ClearSession()
        {
            HttpContext.Session.SetString("userId", "");
            HttpContext.Session.SetString("userId", "");
            ViewBag.userId = null;
            ViewBag.userName = null;
        }

        public IActionResult IsLoginView(string viewName)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                ViewBag.userId = HttpContext.Session.GetString("userId");
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.userId = null;
                return View(viewName);
            }
        }
    }
}
