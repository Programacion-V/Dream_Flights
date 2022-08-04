using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dream_Flights.Models;
using Dreams_Flights.Models;
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
    public class CountriesController : Controller
    {
        // GET: CountriesController
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));
                ViewBag.CountriesModel = LoadCountries();
                return View();
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "You must Log In first",
                    BackUrl = "Login",
                    Text = "Go back to Log In"
                };

                return View("Error");
            }
        }


        private List<CountriesModel> LoadCountries()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_countries", null);
            List<CountriesModel> countriesList = new List<CountriesModel>();

            foreach (DataRow row in ds.Rows)
            {
                countriesList.Add(new CountriesModel()
                {
                    id_country = Convert.ToInt16(row["id_country"]),
                    cou_name = row["cou_name"].ToString(),
                    cou_img = row["cou_img"].ToString()
                });
            }

            return countriesList;
        }

        // GET: CountriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CountriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        public ActionResult Save(string cou_name, IFormFile photo, int id_country)
        {
            string filePath;
            if (photo != null)
            {
                string fileName = cou_name + new FileInfo(photo.FileName).Extension;
                filePath = Path.Combine("Images/CountryFlags/", fileName);
                string localFileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/CountryFlags"), fileName);

                using (var stream = new FileStream(localFileName, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
            }
            else
            {
                filePath = Path.Combine("Images/CountryFlags/", "default.gif");
            }
            List<SqlParameter> param = new List<SqlParameter>()
            {
                //new SqlParameter("@id_country", id_country),
                new SqlParameter("@cou_name", cou_name),
                new SqlParameter("@cou_img", filePath)
            };

            DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_country", param);


            return RedirectToAction("Index", "Countries");
        }









        // GET: CountriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CountriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CountriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
