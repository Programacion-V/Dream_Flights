using Dream_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dream_Flights.Controllers
{
    public class BuyFlightsController : Controller
    {
        // GET: BuyFlightsController
        public ActionResult Index()
        {
            ViewBag.buyflightsList = LoadBuyFlights();
            return View();
        }

        private List<Flight> LoadBuyFlights()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_buy_flights", null);
            List<Flight> buyflightsList = new List<Flight>();

            foreach (DataRow row in ds.Rows)
            {
                buyflightsList.Add(new Flight()
                {
                    fli_number = Convert.ToInt16(row["fli_number"]),
                    air_name = row["air_name"].ToString(),
                    place = row["dep_destination"].ToString(),
                    date = row["dep_date"].ToString(),
                    time = row["dep_time"].ToString(),
                    door = row["dep_door"].ToString(),
                    description = row["sta_description"].ToString()
                });
            }

            return buyflightsList;
        }
        // GET: BuyFlightsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BuyFlightsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyFlightsController/Create
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

        // GET: BuyFlightsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BuyFlightsController/Edit/5
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

        // GET: BuyFlightsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BuyFlightsController/Delete/5
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
