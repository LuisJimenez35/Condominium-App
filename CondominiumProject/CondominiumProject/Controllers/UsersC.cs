using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class UsersC : Controller
    {
        // GET: UsersC
        public ActionResult Index()
        {
            return View();
        }


        public IActionResult CreateUser(string DNI, string FirsName, string Lastname, string email1, string Telephone1, string Telephone2, string Email, string Picture, string Password)
        {
            int validationResult = ValidateAndInsertUser(DNI, FirsName, Lastname, email1, Telephone1, Telephone2, Email, Picture, Password);

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
                    return RedirectToAction("UsersIndex", "RootViews", new { email1 });
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

        public IActionResult DeleteUser(string IdUser, string email)
        {
            int validateres = ValidateDeleteUser(IdUser, email);

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
                    return RedirectToAction("UsersIndex", "RootViews", new { email });
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

        private int ValidateAndInsertUser(string DNI, string FirsName, string Lastname, string email, string Telephone1, string Telephone2, string Email, string Picture, string Password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@DNI", DNI),
                new SqlParameter("@FirsName", FirsName),
                new SqlParameter("@LastName", Lastname),
                new SqlParameter("@Telephone1", Telephone1),
                new SqlParameter("@Telephone2", Telephone2),
                new SqlParameter("@Email", Email),
                new SqlParameter("@Picture", "/AppImages/UserImages/" + Picture),
                new SqlParameter("@Password", Password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndCreateUser", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }

        private int ValidateDeleteUser(string IdUser, string email)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@IdUser", IdUser)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndDeleteUser", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }
    }
}
