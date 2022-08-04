using Dream_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

namespace Dream_Flights.Controllers
{
    public class BuyFlightsController : Controller
    {
        // GET: BuyFlightsController
        public ActionResult Index()
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            {
                ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));
                ViewBag.buyflightsList = LoadBuyFlights();
                ViewBag.ez_transaction = Loadez_transaction();
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
 

        private List<BuyFlight> LoadBuyFlights()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_buy_flights", null);
            List<BuyFlight> buyflightsList = new List<BuyFlight>();
            Random randomID = new Random(Guid.NewGuid().GetHashCode());

            foreach (DataRow row in ds.Rows)
            {
                buyflightsList.Add(new BuyFlight()
                {
                    fli_number = Convert.ToInt16(row["fli_number"]),
                    air_name = row["air_name"].ToString(),
                    destination = row["dep_destination"].ToString(),
                    date = row["dep_date"].ToString(),
                    time = row["dep_time"].ToString(),
                    door = row["dep_door"].ToString(),
                    description = row["sta_description"].ToString(),
                    price = randomID.Next(10, 1000)
            });
            }

            return buyflightsList;
        }

        private ez_transaction Loadez_transaction()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_ez_transaction", null);

            if (ds.Rows.Count == 1)
            {
                ez_transaction ez_Transaction = new ez_transaction()
                {
                    ez_transaction_n = Convert.ToInt16(ds.Rows[0]["ez_n"]),
            
                   
                };

                return ez_Transaction;
            }
            return null;
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
