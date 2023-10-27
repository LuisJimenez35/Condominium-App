using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;

namespace CondominiumProject.Controllers
{
    public class HabitationProjectC : Controller
    {
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

        public IActionResult ViewProject(string IdProject, string email)
        {

            List<HabitationalProjects> projectsList = GetHabitationalProject();

            if (int.TryParse(IdProject, out int projectId))
            {
                var selectedProject = projectsList.FirstOrDefault(p => p.IdProject == projectId);

                if (selectedProject != null)
                {
                    var selectedProjectData = new ProjectData
                    {
                        IdProject = (int)selectedProject.IdProject,
                        Name = selectedProject.Name,
                        Logo = selectedProject.Logo,
                    };

                    TempData["SelectedProjectData"] = JsonConvert.SerializeObject(selectedProjectData);

                    List<HabitationDetails> habitationDetailsList = GetHabitationDetailsForProject(IdProject);

                    // Validar si habitationDetailsList es nulo antes de usarlo
                    if (habitationDetailsList != null)
                    {
                        Console.WriteLine($"Number of habitation details: {habitationDetailsList.Count}");
                        TempData["HabitationDetails"] = JsonConvert.SerializeObject(habitationDetailsList);

                        List<VisitDetail> visitDetailsList = GetVisitDetails(IdProject);
                        if (visitDetailsList != null)
                        {
                            Console.WriteLine($"Number of visit details: {visitDetailsList.Count}");
                            TempData["VisitDetails"] = JsonConvert.SerializeObject(visitDetailsList);
                            return RedirectToAction("ProjectIndex", "RootViews", new { email });
                        }
                        else
                        {
                            Console.WriteLine("VisitDetailsList is null.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("HabitationDetailsList is null.");
                    }
                }
            }
            return RedirectToAction("Error", "RootViews");
        }

        public IActionResult AsignHouse(string IDProyect, string IDHabitation, string email, string IdUser, string username)
        {
            int validationres = ValidateAndAsignHouse(IDProyect, IDHabitation, email, IdUser);

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
                    return View("Error");

                case 3:
                    int sendEmail = SendEmail(username,IDHabitation,email);

                    if(sendEmail == 1)
                    {
                        return RedirectToAction("UsersIndex", "RootViews", new { email });  
                    }
                    else {
                        ViewBag.Error = new ErrorHandler()
                        {
                            Title = "Incorrect Data",
                            ErrorMessage = "Datos duplicados",
                            ActionMessage = "Go to login",
                            Path = "/Login"
                        };
                        return View("ErrorHandler");
                    }
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

        public int SendEmail(string username, string IDHabitation, string email)
        {

            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("soportprimeprogram@gmail.com");
                message.To.Add(email);
                message.Subject = "Register Conformation";

                string body = string.Format("<div style='font-family: Arial, sans-serif; color: #333;'>" +
                             "<h2 style='color: #007BFF;'>Registration Confirmation!</h2>" +
                             "<p>Dear {0},</p>" +
                             "<p>Welcome to our system. Your registration for the room has been successfully confirmed.</p>" +
                             "<p>Below are the details of your registration:</p>" +
                             "<ul>" +
                             "<li><strong>Name:</strong> {0}</li>" +
                             "<li><strong>Email:</strong> {1}</li>" +
                             "<li><strong>Room:</strong> {2}</li>" +
                             "</ul>" +
                             "<p style='background-color: #f8f9fa; padding: 10px; border-radius: 5px;'>" +
                             "Thank you for choosing our platform. If you have any questions, feel free to contact us." +
                             "</p>" +
                             "<p>Best regards,<br>The [Your Platform Name] Team</p>" +
                             "</div>", username, email, IDHabitation);
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("soportprimeprogram@gmail.com", "kihjvhoodlmvgmvw");
                smtpClient.EnableSsl = true;


                smtpClient.Send(message);

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el correo. Error: " + ex.Message);

                return 2;
            }
        }

        private int ValidateAndAsignHouse(string IDProyect, string IDHabitation, string email, string IdUser)
        {
            var projectId = Convert.ToInt32(IDProyect);
            var habitationId = Convert.ToInt32(IDHabitation);
            var userId = Convert.ToInt32(IdUser);

            var queryParameters = new List<SqlParameter>
            {
                new SqlParameter("@IDHabitation", habitationId),
                new SqlParameter("@IDProject", projectId),
                new SqlParameter("@IDUser", userId)
            };

            var resultado = DatabaseHelper.ExecuteQuery("VerifyAndAssignHouse", queryParameters);

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["Result"]);
            }

            return -2;
        }





        private List<HabitationDetails> GetHabitationDetailsForProject(string IdProject)
        {

            var projectId = Convert.ToInt32(IdProject);

            var queryParameters = new List<SqlParameter>
                {
                    new SqlParameter("@ProjectId", projectId)
                };

            // Asegúrate de que ExecuteQuery devuelve List<HabitationDetails>
            var habitationDetailsList = DatabaseHelper.ExecuteQuery<HabitationDetails>("GetHabitationDetailsForProject", queryParameters);

            return habitationDetailsList;
        }

        private List<VisitDetail> GetVisitDetails(string IdProject)
        {
            var projectId = Convert.ToInt32(IdProject);

            var queryParameters = new List<SqlParameter>
            {
                 new SqlParameter("@ProjectId", projectId)
            };

            // Asegúrate de que ExecuteQuery devuelve List<VisitDetail>
            var visitDetailsList = DatabaseHelper.ExecuteQuery<VisitDetail>("GetVisitsByProjectID", queryParameters);

            return visitDetailsList;
        }

    }
}
