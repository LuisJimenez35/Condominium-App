using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace CondominiumProject.Controllers
{
    public class FastVisitC : Controller
    {
        // GET: FastVisitC
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CreateFastVisit(string ServiceName, string Category, string FastVisitDate, string IDHabitation, string IDProject, string email)
        {
            int validationResult = ValiidateAndInsertFastVisit(ServiceName, Category, FastVisitDate, IDHabitation, IDProject,email);

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

        private int ValiidateAndInsertFastVisit(string ServiceName, string Category, string FastVisitDate, string IDHabitation, string IDProject, string email)
        {
            var queryParameters = new List<SqlParameter>
            {
                    new SqlParameter("@ServiceName", ServiceName),
                    new SqlParameter("@Category", Category),
                    new SqlParameter("@FastVisitDate", FastVisitDate),
                    new SqlParameter("@IDHabitation", IDHabitation),
                    new SqlParameter("@IDProject", IDProject)
            };

            var result = DatabaseHelper.ExecuteQuery("spInsertFastVisit", queryParameters);

            return Convert.ToInt32(result.Rows[0][0]);
        }
    }
}
