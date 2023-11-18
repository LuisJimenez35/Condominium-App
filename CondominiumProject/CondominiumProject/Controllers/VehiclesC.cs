using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class VehiclesC : Controller
    {
        // GET: VehiclesC
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CreateVehicle(string Plate, string Model, string Marc,string Color, string IdUser, string email)
        {
            int validationResult = ValidateAndInsertVehicle(Plate, Marc, Model, Color, IdUser, email);

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
                    return RedirectToAction("HomeIndex", "UsersViews", new { email });
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

        private int ValidateAndInsertVehicle(string Plate, string Marc, string Model, string Color, string IdUser, string email)
        {
            var queryParameters = new List<SqlParameter>
            {
                    new SqlParameter("@Plate", Plate),
                    new SqlParameter("@Marc", Marc),
                    new SqlParameter("@Model", Model),
                    new SqlParameter("@Color", Color),
                    new SqlParameter("@IdUser", IdUser)
                };

            var result = DatabaseHelper.ExecuteQuery("spInsertVehicle", queryParameters);

            return Convert.ToInt32(result.Rows[0]["Result"]);
        }

    }  
}
