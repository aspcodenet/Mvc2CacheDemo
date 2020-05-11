using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc2CacheDemo.Models;
using Mvc2CacheDemo.ViewModels;

namespace Mvc2CacheDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
        public IActionResult Index()
        {
            return View();
        }


       [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "num" })]
       public IActionResult Product(int num)
        {
            var model = new ProductViewModel();
            model.Id = num;
            if (model.Id == 1)
            {
                model.Name = "Banan";
                model.Price = 12;
            }
            else if (model.Id == 2)
            {
                model.Name = "Läsk";
                model.Price = 4;
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
