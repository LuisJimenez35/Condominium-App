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

        public IActionResult RootUsersIndex()
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

            ViewBag.RootUsersList = GetRootUsers();

            ViewBag.email = Request.Query["email"].ToString();

            return View();
        }


        public IActionResult CreateProyectIndex()
        {
            ViewBag.email = Request.Query["email"].ToString();

            return View();
        }

        public IActionResult EditProyectIndex()
        {
            ViewBag.email = Request.Query["email"].ToString();

            ViewBag.ProjectCode = Request.Query["projectCode"].ToString();
            ViewBag.ProjectName = Request.Query["projectName"].ToString();
            ViewBag.ProjectAdress = Request.Query["projectAdress"].ToString();
            ViewBag.ProjectTelephone = Request.Query["projectOfficeTelephone"].ToString();

            return View();
        }

        //Funcion para obtener los proyectos habitacionales
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

        public List<RootUsers> GetRootUsers()
        {
            List<RootUsers> rootUsersList = new List<RootUsers>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetRootUsers", null);

            foreach (DataRow dr in ds.Rows)
            {
                rootUsersList.Add(new RootUsers
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString()
                });
            }
            return rootUsersList;
        }


        // Funcion pincipal para crear un proyecto nuevo
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
                    return View("Error");
            }
        }

        // Funcion para editar un proyecto
        public ActionResult EditProject(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
        {
            int validationres = ValidateUpdateProjects(Name, Adress, Code, OfficeTelephone, Logo);

            switch (validationres)
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
                    return View("Error");
            }
        }

        // Funcion para eliminar un proyecto
        public ActionResult DeleteProject(string Code)
        {
            int validationres = ValidateDeleteProjects(Code);

            switch (validationres)
            {
                case 1:
                    ViewBag.Error = new ErrorHandler()
                    {
                        Title = "Incorrect Data",
                        ErrorMessage = "Ningun Proyecto Encontrado",
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
                    return View("Error");
            }
        }

        //Funcion para validar la eliminacion de un proyecto
        private int ValidateDeleteProjects(string Code)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Code", Code)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndDeleteProjectData", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }
            return -2;
        }


        //Funcion para validar la actualizacion de datos de un proyecto
        private int ValidateUpdateProjects(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", Name),
                new SqlParameter("@Adress", Adress),
                new SqlParameter("@Code", Code),
                new SqlParameter("@OfficeTelephone", OfficeTelephone),
                new SqlParameter("@Logo", "/AppImages/ProjectsImages/" + Logo)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndUpdateProjectData", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }
            return -2;
        }

        //Funcion para validar la insercion de un nuevo proyecto
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

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

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
