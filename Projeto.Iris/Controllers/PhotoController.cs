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
    public class PhotoController : Controller
    {
        private IPhotoRepository _photoRepository;
        private IUserRepository _userRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public PhotoController(IPhotoRepository photoRepository, 
                               IUserRepository userRepository, 
                               IWebHostEnvironment webHostEnvironment)
        {
            _photoRepository = photoRepository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                var userId = HttpContext.Session.GetString("userId");

                string serverRootPath = _webHostEnvironment.ContentRootPath;


                if (Directory.Exists(Path.Combine(serverRootPath, "Images/" + userId)))
                {
                    string[] images = Directory.GetFiles(Path.Combine(serverRootPath, "Images/" + userId));
                    if (images.Length > 0)
                    {
                        List<string> base64 = new List<string>();
                        foreach (var image in images)
                        {
                            base64.Add(System.IO.File.ReadAllText(image.ToString()));
                        }

                        ViewBag.images = base64;
                    }
                    else
                    {
                        ViewBag.images = null;
                    }
                }
                else
                {
                    ViewBag.images = null;
                }

                ViewBag.userName = HttpContext.Session.GetString("userName");
                ViewBag.userId = userId;
                return View();
            }
            else
            {
                ViewBag.userId = null;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult SavePhoto(string base64image) 
        {

            var userId = HttpContext.Session.GetString("userId");
            var findUser = _userRepository.FindById(int.Parse(userId));

            string serverRootPath = _webHostEnvironment.ContentRootPath;
            //Cria uma pasta para cada usuario
            Directory.CreateDirectory("Images/" + userId);
            var locationPath = "Images/" + userId + "/" +  DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";

            System.IO.File.WriteAllText(Path.Combine(serverRootPath, locationPath), base64image);
           
            Photo photo = new Photo { PhotoBase64Location = locationPath, UserId = findUser.UserId };

            _photoRepository.Insert(photo);
            _photoRepository.Save();

            return View("Index");
        }

        [HttpPost]
        public IActionResult NewPhotos()
        {
            var userId = HttpContext.Session.GetString("userId");
            string serverRootPath = _webHostEnvironment.ContentRootPath;

            var photos = _photoRepository.ListAllByUserId(int.Parse(userId));
            DirectoryInfo userDirectory = new DirectoryInfo(Path.Combine(serverRootPath, "Images/" + userId));
                            
            //Para novas fotos exclui todas as fotos
            foreach (var photo in photos) 
            {
                _photoRepository.Remove(photo.PhotoId);
                _photoRepository.Save();
            }
            foreach (var file in userDirectory.GetFiles())
            {
                file.Delete();
            }

            return RedirectToAction("Index");
        }
    }
}
