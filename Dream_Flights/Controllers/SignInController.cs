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
    public class SignInController : Controller
    {
        // GET: SignInController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignInUser()
        {
            return View();
        }

        public ActionResult CreateUser(string first_name, string last_name, string pass1, string email)
        {
            try {
                List<SqlParameter> param1 = new List<SqlParameter>();
                param1.Add(new SqlParameter("@email", email));
                DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_email", param1);

                if (ds.Rows.Count >= 1)
                {
                    return StatusCode(409);
                }
                else
                {
                    List<SqlParameter> param2 = new List<SqlParameter>();

                    param2.Add(new SqlParameter("@per_first_name", first_name));
                    param2.Add(new SqlParameter("@per_last_name", last_name));
                    param2.Add(new SqlParameter("@per_email", email));
                    param2.Add(new SqlParameter("@per_img", "Images/Profiles/default.jpg"));
                    param2.Add(new SqlParameter("@user_password", pass1));
                    param2.Add(new SqlParameter("@id_rol", 5));

                    DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_user", param2);
                    return Ok();
                }
            }
            catch(Exception ex) {
                throw ex; 
            }
        
        }

        // GET: SignInController/Details/5
        public ActionResult Details(int id)
        {
            return View();

        }

        // GET: SignInController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignInController/Create
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

        // GET: SignInController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignInController/Edit/5
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

        // GET: SignInController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignInController/Delete/5
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
