using DesafioFull.Services.Interfaces;
using DesafioFull.Web.Models;
using DesafioFull.Web.Models.Titulos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFull.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITituloService _tituloService;
        public HomeController(ILogger<HomeController> logger, ITituloService tituloService)
        {
            _logger = logger;
            _tituloService = tituloService;
        }

        public IActionResult Index()
        {
            return View(_tituloService.GetAll().Select(t => new TituloDetailsViewModel(t)));
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
    }
}
