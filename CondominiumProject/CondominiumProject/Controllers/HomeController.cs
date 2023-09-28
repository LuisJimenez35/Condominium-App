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

		//Ventana de usuario Root
		public IActionResult RootIndex()
		{
            ViewBag.RootHasAccessToProjects = true;

            if (!ViewBag.RootHasAccessToProjects)
            {
                ViewBag.Error = new ErrorHandler()
                {
                    Title = "Security validation",
                    ErrorMessage = "User do not have access to this page",
                    ActionMessage = "Go to login",
                    Path = "/Login"
                };

                return View("ErrorHandler");
            }

            ViewBag.ProjectLists = GetHabitationalProject();

            return View();
        }

        public List<HabitationalProjects> GetHabitationalProject()
        {
            List<HabitationalProjects> projectslist = new List<HabitationalProjects>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetHabitationalProjects", null);

            foreach (DataRow dr in ds.Rows)
            {
                projectslist.Add(new HabitationalProjects
                {
                    IdProject = Convert.ToInt32(dr["IdProject"]),
                    Logo = dr["Logo"].ToString(),                    
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString(),
                    Adress = dr["Adress"].ToString(),
                    OfficeTelephone = dr["OfficeTelephone"].ToString()
                });
            }
            return projectslist;

        }


        //Ventana Guardia Seguridad
        public IActionResult GuardIndex()
		{
			ViewBag.RootHasAccessToProjects = true;

			if (!ViewBag.RootHasAccessToProjects)
			{
				ViewBag.Error = new ErrorHandler()
				{
					Title = "Security validation",
					ErrorMessage = "User do not have access to this page",
					ActionMessage = "Go to login",
					Path = "/Login"
				};

				return View("ErrorHandler");
			}

			ViewBag.ProjectLists = GetHabitationalProject();

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