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
                    Password = row["Password"].ToString(),
                    IDHabitation = Convert.ToInt32(row["IDHabitation"]),
                    ProjectName = row["ProjectName"].ToString()
                });
            }

            List<Vehicles> vehicles = new List<Vehicles>();
            foreach (var user in users)
            {
                var vehicleParameters = new List<SqlParameter>
                {
                    new SqlParameter("@UserId", user.IdUser)
                };

                var vehicleResult = DatabaseHelper.ExecuteQuery("GetVehicleByUserId", vehicleParameters);

                foreach (DataRow vehicleRow in vehicleResult.Rows)
                {
                    vehicles.Add(new Vehicles()
                    {
                        Plate = vehicleRow["Plate"].ToString(),
                        Marc = vehicleRow["Marc"].ToString(),
                        Model = vehicleRow["Model"].ToString(),
                        Color = vehicleRow["Color"].ToString(),
                        IDUser = Convert.ToInt32(vehicleRow["IDUser"])
                    });
                }
            }

            ViewBag.Vehicles = vehicles;
            ViewBag.Users = users;
            return View();
        }


    }
}
