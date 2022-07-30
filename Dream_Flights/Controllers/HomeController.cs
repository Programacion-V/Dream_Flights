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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));


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

        public ActionResult UpdatePhoto(IFormFile photo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")) && photo != null)
            {
                ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));

                string fileName = ViewBag.User.id_person + new FileInfo(photo.FileName).Extension;
                string filePath = Path.Combine("Images/Profiles/", fileName);
                string localFileName = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Profiles"), fileName);

                using (var stream = new FileStream(localFileName, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }

                List<SqlParameter> param = new List<SqlParameter>()
                {
                    new SqlParameter("@id_person", ViewBag.User.id_person),
                    new SqlParameter("@per_img", filePath)
                };

                DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_update_photo", param);

                ViewBag.User.per_img = filePath;

                return View("Index");
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "Unhandled error trying to upload photo",
                    BackUrl = "Home",
                    Text = "Go back to Home"
                };

                return View("Error");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        public ActionResult GetPagesByRol(string id)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@id_rol", id),
            };

            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_GetPagesByRol", param);

            List<Pages> pages = new List<Pages>();

            foreach (DataRow dr in ds.Rows)
            {
                pages.Add(new Pages()
                {
                    Page = dr["page_name"].ToString()
                });
            }

            return Json(pages);
        }

    }
}
