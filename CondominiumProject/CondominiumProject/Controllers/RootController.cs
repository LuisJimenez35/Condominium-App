using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class RootController : Controller
    {
        // GET: RootController
        public ActionResult Index()
        {
            return View();
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


            ViewBag.email = Request.Query["email"].ToString();


            return View();
        }

        public IActionResult CreateProyectIndex()
        {
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

        public ActionResult CreateProject(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
        {
            int validationResult = ValidateTableProjects(Name, Adress, Code, OfficeTelephone, Logo);

            switch (validationResult)
            {
                case 1:
                    ViewBag.Error = new ErrorHandler()
                    {
                        Title = "Incorrect Data",
                        ErrorMessage = "Datos duplicados",
                        ActionMessage = "Go to login",
                        Path = "/Login"
                    };
                    return View("ErrorHandler");
                case 2:
                    return RedirectToAction("RootIndex", "Root");
                case 0:
                    ViewBag.Error = new ErrorHandler()
                    {
                        Title = "Nada",
                        ErrorMessage = "Nada",
                        ActionMessage = "Go to login",
                        Path = "/Login"
                    };
                    return View("ErrorHandler");

                default:
                    // Un resultado desconocido, maneja adecuadamente.
                    return View("Error");
            }
        }

        private int ValidateTableProjects(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", Name),
                new SqlParameter("@Adress", Adress),
                new SqlParameter("@Code", Code),
                new SqlParameter("@OfficeTelephone", OfficeTelephone),
                new SqlParameter("@Logo", "/AppImages/ProjectsImages/" + Logo)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndInsertProjectData", queryParameters);

            // Verifica si hay filas en el resultado
            if (resultado.Rows.Count > 0)
            {
                // Obtiene el valor de Result de la primera fila
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            // Resultado desconocido o error
            return -2;
        }


        // GET: RootController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RootController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RootController/Create
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

        // GET: RootController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RootController/Edit/5
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

        // GET: RootController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RootController/Delete/5
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
