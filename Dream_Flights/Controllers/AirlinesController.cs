using Dream_Flights.Models;
using Dreams_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dream_Flights.Controllers
{
    public class AirlinesController : Controller
    {
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));
                ViewBag.Arlines = LoadAirlines();

                return View();
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "You must LogIn first",
                    BackUrl = "Login",
                    Text = "Go back to LogIn"
                };

                return View("Error");
            }
        }

        private List<Airline> LoadAirlines()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("GetAirlines", null);
            List<Airline> airlineList = new List<Airline>();

            foreach (DataRow row in ds.Rows)
            {
                airlineList.Add(new Airline()
                {
                    Id = Convert.ToInt16(row["Id"]),
                    Name = row["Name"].ToString(),
                    Photo = row["Photo"].ToString(),
                    Phone = row["Phone"].ToString(),
                });
            }

            return airlineList;
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Save(string name, string phone, IFormFile photo)
        {
            string fileName = name + new FileInfo(photo.FileName).Extension;
            string filePath = Path.Combine("/Images/", fileName);
            string localFileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images"), fileName);

            using (var stream = new FileStream(localFileName, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone),
                new SqlParameter("@photo", filePath)
            };

            DatabaseHelper.DatabaseHelper.ExecStoreProcedure("SaveAirline", param);

            return RedirectToAction("Index", "Airlines");
        }
    }
}
