using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CondominiumProject.Controllers
{
	public class GuardController : Controller
	{
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

            ViewBag.email = Request.Query["email"].ToString();

            return View();
		}

		//Obtener lista de proyectos habitacionales
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
	}
}
