using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task6.Models;
using Stripe;
using Task6.Data;

namespace Task6.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private IImageRepository _imageData;

        public HomeController(IImageRepository imageRepository)
        {
            _imageData = imageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            var model = new ViewImage();
            model.Images = _imageData.GetAll();
            return View(model);
        }

        [Route("Home/Payment/{InputId:int}")]
        public IActionResult Payment(int InputId)
        {
            var model = _imageData.GetAll().FirstOrDefault(i => i.Id == InputId);
            return View(model);
        }

        public IActionResult Charge()
        {
            ViewData["Message"] = "Thank you very much for your kind donation, every single cent will help bring them closer to achieving their dreams.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
