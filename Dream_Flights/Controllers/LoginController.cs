using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Dream_Flights.Models;

namespace Dream_Flights.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginUser(string email, string password)
        {
            UserModel user = GetUser(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
                //if(user.rol_name == "")
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = new Models.Error()
                {
                    Message = "Incorrect username or password",
                    BackUrl = "Login",
                    Text = "Try again?"
                };

                return View("Error");
            }
        }

        public UserModel GetUser(string email, string password)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@email", email),
                new SqlParameter("@password", password)
            };

            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_get_user", param);

            if (ds.Rows.Count == 1)
            {
                UserModel user = new UserModel()
                {
                    id_person = ds.Rows[0]["id_person"].ToString(),
                    per_name = ds.Rows[0]["per_name"].ToString(),
                    per_first_name = ds.Rows[0]["per_first_name"].ToString(),
                    per_last_name = ds.Rows[0]["per_last_name"].ToString(),
                    per_email = email,
                    per_img = ds.Rows[0]["per_img"].ToString(),
                    rol_name = ds.Rows[0]["rol_name"].ToString(),
                    id_rol = ds.Rows[0]["id_rol"].ToString(),
                };

                return user;
            }

            return null;
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
