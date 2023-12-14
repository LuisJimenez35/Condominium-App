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

        //View GenerateQrCode
        public IActionResult GenerateQrCode()
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
                    ProjectName = row["ProjectName"].ToString(),
                    IdProject = Convert.ToInt32(row["IdProject"])
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

            List<VisitDetail> visitDetails = new List<VisitDetail>();
            foreach (var user in users)
            {
                var visitParameters = new List<SqlParameter>
                {
                    new SqlParameter("IDHabitation", user.IDHabitation),
                    new SqlParameter("IDProject", user.IdProject)
                };

                var visitResult = DatabaseHelper.ExecuteQuery("spGetVisitDetail", visitParameters);

                foreach (DataRow visitRow in visitResult.Rows)
                {
                    visitDetails.Add(new VisitDetail()
                    {
                        DNI = visitRow["DNI"].ToString(),
                        FirstName = visitRow["FirstName"].ToString(),
                        LastName = visitRow["LastName"].ToString(),
                        VehicleMarc = visitRow["VehicleMarc"].ToString(),
                        VehicleModel = visitRow["VehicleModel"].ToString(),
                        VehicleColor = visitRow["VehicleColor"].ToString(),
                        VehiclePlate = visitRow["VehiclePlate"].ToString(),
                        IDHabitation = Convert.ToInt32(visitRow["IDHabitation"]),
                        VisitDate = Convert.ToDateTime(visitRow["VisitDate"]),
                        VisitTime = TimeSpan.Parse(visitRow["VisitTime"].ToString()),
                        IDProject = Convert.ToInt32(visitRow["IDProject"])
                    });
                }
            }

            List<FastVisitss> fastVisitsses = new List<FastVisitss>();
            foreach (var user in users)
            {
                var fastVisitParameters = new List<SqlParameter>
                {
                    new SqlParameter("IDHabitation", user.IDHabitation),
                    new SqlParameter("IDProject", user.IdProject)
                };

                var fastVisitResult = DatabaseHelper.ExecuteQuery("spGetFastVisit", fastVisitParameters);

                foreach (DataRow fastVisitRow in fastVisitResult.Rows)
                {
                    fastVisitsses.Add(new FastVisitss()
                    {
                        ServiceName = fastVisitRow["ServiceName"].ToString(),
                        Category = fastVisitRow["Category"].ToString(),
                        FastVisitDate = Convert.ToDateTime(fastVisitRow["FastVisitDate"]),
                        IDHabitation = Convert.ToInt32(fastVisitRow["IDHabitation"]),
                        IDProject = Convert.ToInt32(fastVisitRow["IDProject"])
                    });
                }
            }

            List<FavoriteVisits> favoriteVisits = new List<FavoriteVisits>();
            foreach (var user in users)
            {
                var favoriteVisitParameters = new List<SqlParameter>
                {
                    new SqlParameter("IDHabitation", user.IDHabitation),
                };

                var favoriteVisitResult = DatabaseHelper.ExecuteQuery("spGetFavortiteVisit", favoriteVisitParameters);

                foreach (DataRow favoriteVisitRow in favoriteVisitResult.Rows)
                {
                    favoriteVisits.Add(new FavoriteVisits()
                    {
                        FirstName = favoriteVisitRow["FirstName"].ToString(),
                        LastName = favoriteVisitRow["LastName"].ToString(),
                        Kinship = favoriteVisitRow["Kinship"].ToString(),
                        VehicleMarc = favoriteVisitRow["VehicleMarc"].ToString(),
                        VehicleModel = favoriteVisitRow["VehicleModel"].ToString(),
                        VehicleColor = favoriteVisitRow["VehicleColor"].ToString(),
                        VehiclePlate = favoriteVisitRow["VehiclePlate"].ToString(),
                        IDHabitation = Convert.ToInt32(favoriteVisitRow["IDHabitation"]),
                    });
                }
            }


            ViewBag.FavoriteVisits = favoriteVisits;
            ViewBag.FastVisitsses = fastVisitsses;
            ViewBag.VisitDetails = visitDetails;
            ViewBag.Vehicles = vehicles;
            ViewBag.Users = users;
            return View();
        }


    }
}
