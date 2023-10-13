using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class UserGuard : Controller
    {
        // GET: UserGuard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateGuard(string UserName, string Password, string email)
        {
            int validationResult = ValidateAndInsertGuard(UserName, Password);

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
                    return RedirectToAction("GuardsIndex", "RootViews", new { email });
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

        public ActionResult EditGuard(string IDGuard , string Username , string Password, string email)
        {
            int validateres = ValidateUpdateGuard(IDGuard,Username,Password,email);

            switch(validateres)
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
                    return RedirectToAction("GuardsIndex", "RootViews", new { email });
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

        public ActionResult DeleteGuard(string IDGuard, string email)
        {
            int validateres = ValidateDeleteGuard(IDGuard);

            switch (validateres)
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
                    return RedirectToAction("GuardsIndex", "RootViews", new { email });
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


        private int ValidateAndInsertGuard(string UserName, string Password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@Password", Password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndCreateGuard", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }

        private int ValidateUpdateGuard(string IDGuard, string UserName, string Password, string email)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@IDGuard", IDGuard),
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@Password", Password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndUpdateGuard", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }

        private int ValidateDeleteGuard(string IDGuard)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@IDGuard", IDGuard)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndDeleteGuard", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }

        // GET: UserGuard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserGuard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserGuard/Create
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

        // GET: UserGuard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserGuard/Edit/5
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

        // GET: UserGuard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserGuard/Delete/5
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
