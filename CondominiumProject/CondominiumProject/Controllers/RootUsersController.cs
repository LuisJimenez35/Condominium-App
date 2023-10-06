using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class RootUsersController : Controller
    {
        // Funcion para crear un nuevo usuario root
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
                    return RedirectToAction("RootUsersIndex", "RootViews");
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

        // Funcion para editar un usuario root
        public ActionResult EditUserRoot(string ID, string UserName, string Password)
        {
            int validationres = ValidateUpdateRoot(ID,UserName,Password);

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
                    return RedirectToAction("RootUsersIndex", "RootViews");
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

        //Funcion para eliminar un usuario root
        public ActionResult DeleteUserRoot(string ID)
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
                    return RedirectToAction("RootUsersIndex", "RootViews");
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

        //Funcion para validar la creacion de un nuevo usuario root
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

        //Funcion para validar la edicion de un usuario root
        private int ValidateUpdateRoot(string ID, string UserName, string Password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", ID),
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@Password", Password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndUpdateRoot", queryParameters);

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
    }
}
