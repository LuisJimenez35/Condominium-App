using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CondominiumProject.Controllers
{
    public class UsersViews : Controller
    {
        // GET: UsersViewsC
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult HomeIndex(string email)
        {
            List<Users> users = new List<Users>();
            var queryParameters = new List<SqlParameter>
                {
                    new SqlParameter("@Email", email)
                };

            var result = DatabaseHelper.ExecuteQuery("spGetUsersByEmail", queryParameters);

            foreach (DataRow row in result.Rows)
            {
                users.Add(new Users()
                {
                    IdUser = Convert.ToInt32(row["IdUser"]),
                    DNI = row["DNI"].ToString(),
                    FirsName = row["FirsName"].ToString(), 
                    LastName = row["Lastname"].ToString(),
                    Telephone1 = row["Telephone1"].ToString(),
                    Telephone2 = row["Telephone2"].ToString(),
                    Email = row["Email"].ToString(),
                    Picture = row["Picture"].ToString(),
                    Password = row["Password"].ToString()
                });
            }

            ViewBag.Users = users;
            return View();
        }


    }
}
