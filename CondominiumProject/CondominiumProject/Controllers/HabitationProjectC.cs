using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;


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
            try
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

                        Console.WriteLine($"Selected Project: {selectedProject.Name}");
                        return RedirectToAction("ProjectIndex", "RootViews", new { email });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ViewProject: {ex.Message}");
            }
            return RedirectToAction("Error", "RootViews");
        }




    }
}
