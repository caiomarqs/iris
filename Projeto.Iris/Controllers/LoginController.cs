using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Iris.Models;
using Projeto.Iris.Repositories;
using Projeto.Iris.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Iris.Controllers
{
    public class LoginController : Controller
    {

        private IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetLogin(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = _userRepository.FindByEmail(model.EmailLogin);

                if (user != null)
                {
                    //decrypt na sennha
                    bool passwordTest = BCrypt.Net.BCrypt.Verify(model.PasswordLogin, user.Password);

                    if (passwordTest)
                    {
                        SetSession(user);
                        return RedirectToAction("Index", "User");

                    }
                    else
                    {
                        TempData["login-error"] = "A senha não está correta!";
                    }
                }
                else
                {
                    TempData["login-error"] = "Email não cadastrado!";
                }
            }

            return View("Index");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            ClearSession();
            return View("Index");
        }


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
    }
}
