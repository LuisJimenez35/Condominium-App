using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CondominiumProject.Controllers
{
    public class RootViewsController : Controller
    {

        //Ventana principal de usuario Root
        public IActionResult RootIndex()
        {
            ViewBag.RootHasAccessToProjects = true;

            if (!ViewBag.RootHasAccessToProjects)
            {
                ViewBag.Error = new ErrorHandler()
                {
                    Title = "Security validation",
                    ErrorMessage = "User do not have access to this page",
                    ActionMessage = "Go to login",
                    Path = "/Login"
                };

                return View("ErrorHandler");
            }

            ViewBag.ProjectLists = GetHabitationalProject();


            ViewBag.email = Request.Query["email"].ToString();

            return View();
        }

        //Ventana usuarios root
        public IActionResult RootUsersIndex()
        {
            ViewBag.RootHasAccessToProjects = true;

            ViewBag.RootUsersList = GetRootUsers();

            ViewBag.email = Request.Query["email"].ToString();

            return View();
        }

        public IActionResult GuardsIndex()
        {
            ViewBag.RootHasAccessToProjects = true;
            ViewBag.GuardsList = GetGuards();
            ViewBag.email = Request.Query["email"].ToString();
            return View();
        }

        public IActionResult UsersIndex()
        {
            ViewBag.RootHasAccessToProjects = true;
            ViewBag.UsersList = GetUsers();
            ViewBag.ProjectLists = GetHabitationalProject();
            ViewBag.email = Request.Query["email"].ToString();
            return View();
        }

        public IActionResult ProjectIndex(string email)
        {
            if (TempData.TryGetValue("SelectedProjectData", out object jsonDataObj) && jsonDataObj is string jsonData)
            {
                var selectedProjectData = JsonConvert.DeserializeObject<ProjectData>(jsonData);
                ViewBag.email = email;

                // Verificar si hay detalles de habitación en TempData
                if (TempData.TryGetValue("HabitationDetails", out object habitationDetailsDataObj) && habitationDetailsDataObj is string habitationDetailsData)
                {
                    var habitationDetailsList = JsonConvert.DeserializeObject<List<HabitationDetails>>(habitationDetailsData);
                    ViewBag.HabitationDetailsList = habitationDetailsList;

                    if (TempData.TryGetValue("VisitDetails", out object visitDetailsDataObj) && visitDetailsDataObj is string visitDetailsData)
                    {
                        var visitDetailsList = JsonConvert.DeserializeObject<List<VisitDetail>>(visitDetailsData);
                        ViewBag.VisitDetailsList = visitDetailsList;
                    }
                }
                return View(selectedProjectData);
            }
            return View();
        }

        //Funcion para obtener los proyectos habitacionales
        public List<HabitationalProjects> GetHabitationalProject()
        {
            List<HabitationalProjects> projectslist = new List<HabitationalProjects>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetHabitationalProjects", null);

            foreach (DataRow dr in ds.Rows)
            {
                projectslist.Add(new HabitationalProjects
                {
                    IdProject = Convert.ToInt32(dr["IdProject"]),
                    Logo = dr["Logo"].ToString(),
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString(),
                    Adress = dr["Adress"].ToString(),
                    OfficeTelephone = dr["OfficeTelephone"].ToString()
                });
            }
            return projectslist;
        }

        //Funcion para obtener los usuarios root
        public List<RootUsers> GetRootUsers()
        {
            List<RootUsers> rootUsersList = new List<RootUsers>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetRootUsers", null);

            foreach (DataRow dr in ds.Rows)
            {
                rootUsersList.Add(new RootUsers
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString()
                });
            }
            return rootUsersList;
        }

        public List<Guards> GetGuards()
        {
            List<Guards> guardsList = new List<Guards>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetGuards", null);

            foreach (DataRow dr in ds.Rows)
            {
                guardsList.Add(new Guards
                {
                    IDGuard = Convert.ToInt32(dr["IDGuard"]),
                    UserName = dr["UserName"].ToString(),
                    Password = dr["Password"].ToString()
                });
            }
            return guardsList;
        }

        public List<Users> GetUsers()
        {
            List<Users> usersList = new List<Users>();
            DataTable ds = Database.DatabaseHelper.ExecuteQuery("spGetUsers", null);

            foreach (DataRow dr in ds.Rows)
            {
                int? idHabitation = dr["IDHabitation"] != DBNull.Value ? Convert.ToInt32(dr["IDHabitation"]) : (int?)null;
                string projectName = dr["ProjectName"] != DBNull.Value ? dr["ProjectName"].ToString() : null;

                usersList.Add(new Users
                {
                    IdUser = Convert.ToInt32(dr["IdUser"]),
                    DNI = dr["DNI"].ToString(),
                    FirsName = dr["FirsName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    Telephone1 = dr["Telephone1"].ToString(),
                    Telephone2 = dr["Telephone2"].ToString(),
                    Email = dr["Email"].ToString(),
                    Picture = dr["Picture"].ToString(),
                    Password = dr["Password"].ToString(),
                    IDHabitation = idHabitation,
                    ProjectName = projectName
                });
            }
            return usersList;
        }
    }
}
