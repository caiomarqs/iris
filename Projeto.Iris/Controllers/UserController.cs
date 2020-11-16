using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projeto.Iris.Repositories;
using Projeto.Iris.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Projeto.Iris.Controllers
{
    public class UserController : Controller
    {

        private IUserRepository _userRepository;
        private IPhotoRepository _photoRepository;
        private IWebHostEnvironment _webHostEnvironment;


        public UserController(IUserRepository userRepository, 
                              IPhotoRepository photoRepository,
                              IWebHostEnvironment webHostEnvironment) 
        {
            _userRepository = userRepository;
            _photoRepository = photoRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                ViewBag.userId = HttpContext.Session.GetString("userId");
                ViewBag.userName = HttpContext.Session.GetString("userName");
                return View();
            }
            else
            {
                ViewBag.userId = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetString("userId");

            ViewBag.userId = userId;
            ViewBag.userName = HttpContext.Session.GetString("userName");

            var findUser = _userRepository.FindById(int.Parse(userId));
            return View();
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {

            var findUser = _userRepository.FindByEmail(user.Email);

            if (findUser == null)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = passwordHash;
                _userRepository.Update(user);
                _userRepository.Save();
                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["error-msg"] = "Email já cadastrado!";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Remove(int userId) 
        {
            string serverRootPath = _webHostEnvironment.ContentRootPath;
            var photos = _photoRepository.ListAllByUserId(userId);
            DirectoryInfo userDirectory = new DirectoryInfo(Path.Combine(serverRootPath, "Images/" + userId));

            foreach (var photo in photos)
            {
                _photoRepository.Remove(photo.PhotoId);
                _photoRepository.Save();
            }
            foreach (var file in userDirectory.GetFiles())
            {
                file.Delete();
            }

            _userRepository.Remove(userId);
            _userRepository.Save();
            
            ClearSession();

            return RedirectToAction("Index", "Home");
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
