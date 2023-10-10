using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class ProyectsController : Controller
    {
        // Funcion para crear un proyecto nuevo
        public ActionResult CreateProject(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
        {
            int validationResult = ValidateCreateProjects(Name, Adress, Code, OfficeTelephone, Logo);

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
                    return RedirectToAction("RootIndex", "RootViews");
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
        public ActionResult EditProject(string Name, string Adress, string Code, string OfficeTelephone, string Logo ,string email)
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
                    return RedirectToAction("RootIndex", "RootViews");
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
        public ActionResult DeleteProject(string Code , string email)
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
                    return RedirectToAction("RootIndex", "RootViews");
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

        //Funcion para validar la insercion de un nuevo proyecto
        private int ValidateCreateProjects(string Name, string Adress, string Code, string OfficeTelephone, string Logo)
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
    }
}
