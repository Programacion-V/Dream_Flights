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

        public CreateUserModel CreateUser(string first_name, string last_name, string pass1,string pass2, string email)
        {
            if (pass1 == pass2)
            {

                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter("@per_first_name", first_name));
                param.Add(new SqlParameter("@per_last_name", last_name));
                param.Add(new SqlParameter("@per_email", email));
                param.Add(new SqlParameter("@per_img", "default.jpg"));
                param.Add(new SqlParameter("@user_password", pass1));
                param.Add(new SqlParameter("@id_rol", 5));


                DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_user", param);

                return null;
            }
            else {


                return null;
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
