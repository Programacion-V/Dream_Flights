using Dream_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dream_Flights.Controllers
{
    public class AdminUsersController : Controller
    {
        // GET: AdminUsersController
        public ActionResult Index()
        {
            ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));
            ViewBag.Users = LoadUsers();
            ViewBag.Rols = LoadRols();
            return View();
        }

        public List<UserModel> LoadUsers()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_users", null);
            List<UserModel> userList = new List<UserModel>();

            foreach (DataRow row in ds.Rows)
            {
                userList.Add(new UserModel()
                {
                    id_user = row["id_user"].ToString(),
                    id_person = row["id_person"].ToString(),
                    per_first_name = row["per_first_name"].ToString(),
                    per_last_name = row["per_last_name"].ToString(),
                    per_email = row["per_email"].ToString(),
                    per_img = null,
                    rol_name = row["rol_name"].ToString(),
                    id_rol = row["id_rol"].ToString(),

                });
            }

            return userList;
        }


        private List<SelectListItem> LoadRols()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_roles", null);
            List<SelectListItem> rolList = new List<SelectListItem>();

            foreach (DataRow row in ds.Rows)
            {
                rolList.Add(new SelectListItem()
                {
                    Text = row["rol_name"].ToString(),
                    Value = row["id_rol"].ToString(),

                });
            }

            return rolList;
        }

        // GET: AdminUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminUsersController/Create
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

        // GET: AdminUsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminUsersController/Edit/5
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

        // GET: AdminUsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminUsersController/Delete/5
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
