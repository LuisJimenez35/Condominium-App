using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class RootAdminsController : Controller
    {
        // GET: RootAdminsController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRootUser(string UserName, string Password)
        {
            int validationResult = ValidateAndInsertRoots(UserName, Password);

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
                    return RedirectToAction("RootUsersIndex", "Root");
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

        public ActionResult DeleteProject(string ID)
        {
            int validationres = ValidateDeleteRoot(ID);

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
                    return RedirectToAction("RootUsersIndex", "Root");
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


        private int ValidateAndInsertRoots(string UserName, string Password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@Password", Password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndCreateRoot", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }

        //Funcion para validar la eliminacion de un proyecto
        private int ValidateDeleteRoot(string ID)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", ID)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndDeleteRoot", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }
            return -2;
        }

        // GET: RootAdminsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RootAdminsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RootAdminsController/Create
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

        // GET: RootAdminsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RootAdminsController/Edit/5
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

        // GET: RootAdminsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RootAdminsController/Delete/5
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
