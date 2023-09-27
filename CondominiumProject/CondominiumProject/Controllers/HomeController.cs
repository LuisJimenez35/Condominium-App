using CondominiumProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CondominiumProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		//Ventana de usuario Root
		public IActionResult RootIndex()
		{
			return View();
		}
		//Ventana Guardia Seguridad
		public IActionResult GuardIndex()
		{
			return View();
		}
		//Ventana Usuario basico
		public IActionResult UserIndex()
		{
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