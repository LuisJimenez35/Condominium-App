using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CondominiumProject.Controllers
{
	public class GuardController : Controller
	{
		// GET: GuardController
		public ActionResult Index()
		{
			return View();
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



		// GET: GuardController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: GuardController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: GuardController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: GuardController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: GuardController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: GuardController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: GuardController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
