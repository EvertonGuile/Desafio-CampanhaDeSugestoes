using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aula_5.Models;
using Microsoft.AspNetCore.Http;

namespace aula_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
                        // Verificar se o cookie de consentimento já foi aceito
            if (Request.Cookies["CookieConsent"] == null)
            {
                // Criar um novo cookie de consentimento
                var cookieOptions = new CookieOptions
                {
                    // Definir a duração do cookie (por exemplo, 30 dias)
                    Expires = DateTime.Now.AddDays(30)
                };

                // Definir o valor do cookie para indicar que o consentimento foi aceito
                Response.Cookies.Append("CookieConsent", "true", cookieOptions);
            }

            
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
    }
}
