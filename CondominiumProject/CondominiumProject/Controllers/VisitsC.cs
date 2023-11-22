using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class VisitsC : Controller
    {
        // GET: VisitsC
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CreateVisit(string DNI, string FirstName, string LastName, string Marc, string Model, string Color, string Plate, string Date, string Hour, string email,string IDHabitation,string IdProject)
        {
            int validationResult = ValidateAndInsertVisit(DNI, FirstName,LastName,Marc,Model,Color,Plate,Date,Hour,email,IDHabitation, IdProject) ;

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

        private int ValidateAndInsertVisit(string DNI, string FirstName, string LastName, string Marc, string Model, string Color, string Plate, string Date, string Hour, string email,string IDHabitation, string IdProject)
        {
            var queryParameters = new List<SqlParameter>
            {
                    new SqlParameter("@DNI", DNI),
                    new SqlParameter("@FirstName", FirstName),
                    new SqlParameter("@LastName", LastName),
                    new SqlParameter("@Marc", Marc),
                    new SqlParameter("@Model", Model),
                    new SqlParameter("@Color", Color),
                    new SqlParameter("@Plate", Plate),
                    new SqlParameter("@Date", Date),
                    new SqlParameter("@Hour", Hour),
                    new SqlParameter("@IDHabitation", IDHabitation),
                    new SqlParameter("@IDProject", IdProject)
                };

            var result = DatabaseHelper.ExecuteQuery("spInsertVisit", queryParameters);

            return Convert.ToInt32(result.Rows[0]["Result"]);
        }
    }
}
