using CondominiumProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace CondominiumProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
    
		//Ventana Usuario basico
		public IActionResult UserIndex()
		{
            ViewBag.email = Request.Query["email"].ToString();

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