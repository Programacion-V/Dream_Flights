using Dream_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dream_Flights.Controllers
{
    public class StartedController : Controller
    {
        // GET: StartedController1
        public ActionResult Index()
        {
            ViewBag.Started = LoadStarted();
            return View();
        }

        private List<Started> LoadStarted()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_imagesStarted", null);
            List<Started> StartedList = new List<Started>();

            foreach (DataRow row in ds.Rows)
            {
                StartedList.Add(new Started()
                {
                    Id = Convert.ToInt16(row["Id"]),
                    Photo = row["Photo"].ToString(),
                });
            }

            return StartedList;
        }

        // GET: StartedController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StartedController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StartedController1/Create
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

        // GET: StartedController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StartedController1/Edit/5
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

        // GET: StartedController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StartedController1/Delete/5
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
