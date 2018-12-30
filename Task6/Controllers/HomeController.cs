using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task6.Models;
using Stripe;
using Task6.Data;
using Microsoft.Extensions.Configuration;

namespace Task6.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private IImageRepository _imageData;
        private IConfiguration _configuration;

        public HomeController(IImageRepository imageRepository, IConfiguration configuration)
        {
            _imageData = imageRepository;
            _configuration = configuration;
        }

        #region View Controllers
        //1. Index View
        public IActionResult Index()
        {
            var model = new ImageViewModel();
            model.Message = _configuration["Message"];
            return View(model);
        }

        //2. Gallery View
        public IActionResult Gallery()
        {
            var model = new ImageViewModel();
            model.Images = _imageData.GetAll();
            return View(model);
        }

        //3. Payment View
        [Route("Home/Payment/{InputId:int}")]
        public IActionResult Payment(int InputId)
        {
            var model = _imageData.GetAll().FirstOrDefault(i => i.Id == InputId);
            return View(model);
        }

        //4. Charge View
        public IActionResult Charge()
        {
            ViewData["Message"] = "Thank you very much for your kind donation, every single cent will help bring them closer to achieving their dreams.";
            return View();
        }

        //5. Error View
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
