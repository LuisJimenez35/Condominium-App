using CondominiumProject.Database;
using CondominiumProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using static QRCoder.PayloadGenerator;
using System.Net.Mail;
using System.Net;



namespace CondominiumProject.Controllers
{
    public class VisitsC : Controller
    {
        // GET: VisitsC
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult CreateVisit(string DNI, string FirstName, string LastName, string Marc, string Model, string Color, string Plate, string Date, string Hour, string email, string IDHabitation, string IdProject)
        {
            int validationResult = ValidateAndInsertVisit(DNI, FirstName, LastName, Marc, Model, Color, Plate, Date, Hour, email, IDHabitation, IdProject);

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



        public IActionResult GenerateQrCode(string email, string FullName, string DNI, string Date, string Hour, string IDHabitation, string ProjectName, string Plate)
        {
            try
            {
                Random random = new Random();
                int numeroAleatorio = random.Next(1000, 10000);

                string visitDetails = $"{FullName},{email},{DNI},{Date},{Hour},{IDHabitation},{ProjectName},{Plate},{numeroAleatorio}";

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(visitDetails, QRCodeGenerator.ECCLevel.Q);

                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Convierte la imagen a bytes
                using (MemoryStream ms = new MemoryStream())
                {
                    qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    String BasicQr = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    String BasicNumber = numeroAleatorio.ToString();

                    int code = SendQuery(email, FullName, DNI, Date, Hour, IDHabitation, ProjectName, Plate, BasicNumber, BasicQr);

                    switch (code)
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
                            return RedirectToAction("HomeIndex", "UsersViews", new { email });
                        default:
                            return RedirectToAction("HomeIndex", "UsersViews", new { email });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el correo. Error: " + ex.Message);
            }

            return View("ErrorView");
        }

        private int SendQuery(string email, string FullName, string DNI, string Date, string Hour, string IDHabitation, string ProjectName, string Plate, string BasicNumber, string BasicQr)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("soportprimeprogram@gmail.com");
                message.To.Add(email);
                message.Subject = "Visit Secret Code";

                string body = string.Format("<div style='font-family: Arial, sans-serif; color: #333;'>" +
                "<h2 style='color: #007BFF;'>Visit Information</h2>" +
                "<p>{0},</p>" +
                "<p>Thank you for visiting us. Below are the details of your visit:</p>" +
                "<ul>" +
                "<li><strong>Name:</strong> {0}</li>" +
                "<li><strong>Visit DNI:</strong> {1}</li>" +
                "<li><strong>Date:</strong> {2}</li>" +
                "<li><strong>Hour:</strong> {3}</li>" +
                "<li><strong>Room ID:</strong> {4}</li>" +
                "<li><strong>Project Name:</strong> {5}</li>" +
                "<li><strong>Plate:</strong> {6}</li>" +
                "</ul>" +
                "<p><strong>Secret Key :</strong> {7}</p>" +
                "<p style='background-color: #f8f9fa; padding: 10px; border-radius: 5px;'>" +
                "</p>" +
                "<p>Best regards,<br>The [Your Company Name] Team</p>" +
                "</div>", FullName, DNI, Date, Hour, IDHabitation, ProjectName, Plate, BasicNumber);

                message.Body = body;
                message.IsBodyHtml = true;

                Attachment qrAttachment = new Attachment(new MemoryStream(Convert.FromBase64String(BasicQr.Split(',')[1])), "qrcode.png", "image/png");
                message.Attachments.Add(qrAttachment);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("soportprimeprogram@gmail.com", "kihjvhoodlmvgmvw");
                smtpClient.EnableSsl = true;

                smtpClient.Send(message);

                return 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el correo. Error: " + ex.Message);          
            }

            return 0;
        }

        private int ValidateAndInsertVisit(string DNI, string FirstName, string LastName, string Marc, string Model, string Color, string Plate, string Date, string Hour, string email, string IDHabitation, string IdProject)
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
