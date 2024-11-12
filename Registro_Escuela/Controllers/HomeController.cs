using Microsoft.AspNetCore.Mvc;
using Registro_Escuela.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;

namespace Registro_Escuela.Controllers

{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Administrador, Tutor")]
        public IActionResult Actividades()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Materiales()
        {
            return View();
        }
        [Authorize(Roles = "Administrador, Tutor")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
