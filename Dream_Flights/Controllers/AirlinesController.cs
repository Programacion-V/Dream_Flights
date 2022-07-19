using Dream_Flights.Models;
using Dreams_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                ViewBag.CountriesModel = LoadCountries();
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
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_airlines", null);
            List<Airline> airlineList = new List<Airline>();

            foreach (DataRow row in ds.Rows)
            {
                airlineList.Add(new Airline()
                {
                    id_airline = Convert.ToInt16(row["id_airline"]),
                    air_name = row["air_name"].ToString(),
                    air_img = row["air_img"].ToString(),
                    air_phone = row["air_phone"].ToString(),
                    air_country = row["cou_name"].ToString()
                });
            }

            return airlineList;
        }

        public ActionResult Edit()
        {
           
            return View();
        }

        private List<SelectListItem> LoadCountries()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_countries", null);
            List<SelectListItem> countriesList = new List<SelectListItem>();

            foreach (DataRow row in ds.Rows)
            {
                countriesList.Add(new SelectListItem()
                {
                    Text = row["cou_name"].ToString(),
                    Value = row["id_country"].ToString(),

                });
            }

            return countriesList;
        }

        public ActionResult Save(string name, string phone, IFormFile photo, int id_country)
        {
            string filePath;
            if (photo != null)
            {
                string fileName = name + new FileInfo(photo.FileName).Extension;
                filePath = Path.Combine("Images/", fileName);
                string localFileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images"), fileName);

                using (var stream = new FileStream(localFileName, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
            }
            else
            {
                filePath = Path.Combine("/Images/", "default.jpg");
            }
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone),
                new SqlParameter("@photo", filePath),
                new SqlParameter("@id_country", id_country)
            };

            DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_airline", param);

            return RedirectToAction("Index", "Airlines");
        }
    }
}