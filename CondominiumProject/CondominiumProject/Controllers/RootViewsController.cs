﻿using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

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


        public IActionResult CreateProyectIndex()
        {
            ViewBag.email = Request.Query["email"].ToString();

            return View();
        }

        public IActionResult EditProyectIndex()
        {
            ViewBag.email = Request.Query["email"].ToString();

            ViewBag.ProjectCode = Request.Query["projectCode"].ToString();
            ViewBag.ProjectName = Request.Query["projectName"].ToString();
            ViewBag.ProjectAdress = Request.Query["projectAdress"].ToString();
            ViewBag.ProjectTelephone = Request.Query["projectOfficeTelephone"].ToString();

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
    }
}