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
