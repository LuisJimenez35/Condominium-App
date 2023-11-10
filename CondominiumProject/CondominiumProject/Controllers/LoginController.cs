using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using System.ComponentModel.Design;

namespace CondominiumProject.Controllers
{
    public class LoginController : Controller
    {
        // Vista de la ventana de login form
        public ActionResult Index()
        {
            return View();
        }

        //Funcion que valida el login y redirige a la vista correspondiente
        public ActionResult Login(string email, string password)
        {
            int validationResult = ValidateCredentialsRoot(email, password);

            switch (validationResult)
            {
                case 1:
                    return RedirectToAction("RootIndex", "RootViews" , new {email});
                    

                case 0:
                    validationResult = ValidateCredentialsUser(email, password);

                    if (validationResult == 1)
                    {
                        return RedirectToAction("HomeIndex", "UsersViews" , new {email}); 
                    }
                    else 
                    {
                        int validationResult2 = ValidateCredentialsGuard(email, password);

                        if ( validationResult2 == 1)
                        {
                            return RedirectToAction("GuardIndex", "Guard" , new {email});
                        }
                        else
                        {
                            ViewBag.Error = new ErrorHandler()
                            {
                                Title = "Invalid login",
                                ErrorMessage = "Incorrect email or password",
                                ActionMessage = "Go to login",
                                Path = "/Login"
                            };
                            return View("ErrorHandler");
                        }
                    }

                default:
                    // Un resultado desconocido, maneja adecuadamente.
                    return View("Error");
            }
        }

        //Funcion que redirige a la vista de login
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Login");
            
        }   

        //Funcion que valida el login de root
        private int ValidateCredentialsRoot(string email, string password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyLoginRoot", queryParameters);

            if (resultado.Rows.Count == 1)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }


            // Resultado desconocido o error
            return -2;
        }

        //Funcion que valida el login de usuario
        private int ValidateCredentialsUser(string email, string password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyLoginUser", queryParameters);

            if (resultado.Rows.Count == 1)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            // Resultado desconocido o error
            return -2;
        }

        //Funcion que valida el login de guardia
        private int ValidateCredentialsGuard(string email, string password)
        {
            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@Email", email),
                new SqlParameter("@Password", password)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyLoginGuard", queryParameters);

            if (resultado.Rows.Count == 1)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            // Resultado desconocido o error
            return -2;
        }   
    }
}
