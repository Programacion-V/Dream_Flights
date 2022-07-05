using Dream_Flights.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace Dream_Flights.Controllers
{
    public class FlightsController : Controller
    {
        // GET: FlightsController1
        public ActionResult Index()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("user")))
            // {
            // ViewBag.User = JsonSerializer.Deserialize<UserModel>(HttpContext.Session.GetString("user"));
                ViewBag.AirlineSelectListItem = LoadAirlines();
                ViewBag.StatusASelectListItem = LoadStatus_A();
                ViewBag.StatusDSelectListItem = LoadStatus_D();
                ViewBag.flightsListArrivals = LoadFlightsArrivals();
                ViewBag.flightsListDepartures = LoadFlightsDepartures();
                return View();
           // }
           // else
           // {
               //ViewBag.Error = new Models.Error()
                //{
                   // Message = "You must LogIn first",
                   // BackUrl = "Login",
                    //Text = "Go back to LogIn"
                //};

                //return View("Error");
            //}
        }

        private List<SelectListItem> LoadAirlines()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_airlines", null);
            List<SelectListItem> airlineList = new List<SelectListItem>();

            foreach (DataRow row in ds.Rows)
            {
                airlineList.Add(new SelectListItem()
                {
                    Text = row["air_name"].ToString(),
                    Value = row["id_airline"].ToString(),

                });
            }

            return airlineList;
        }

        private List<SelectListItem> LoadStatus_A()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_status_arrivals", null);
            List<SelectListItem> statusAList = new List<SelectListItem>();

            foreach (DataRow row in ds.Rows)
            {
                statusAList.Add(new SelectListItem()
                {
                    Text = row["sta_description"].ToString(),
                    Value = row["id_status"].ToString(),

                });
            }

            return statusAList;
        }

        private List<SelectListItem> LoadStatus_D()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_status_departures", null);
            List<SelectListItem> statusDList = new List<SelectListItem>();

            foreach (DataRow row in ds.Rows)
            {
                statusDList.Add(new SelectListItem()
                {
                    Text = row["sta_description"].ToString(),
                    Value = row["id_status"].ToString(),

                });
            }

            return statusDList;
        }

        public ActionResult SaveFlightArrival(string origen, string origen_date, string origen_time, string origen_door,int statusA,int id_airline)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@id_airline", id_airline),
                new SqlParameter("@arr_date", origen_date),
                new SqlParameter("@arr_time", origen_time),
                new SqlParameter("@arr_door", origen_door),
                new SqlParameter("@arr_origen", origen),
                new SqlParameter("@id_status", statusA),
            };

            DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_flight_arrival", param);

            return RedirectToAction("Index", "Flights");
        }

        public ActionResult SaveFlightDeparture(string destination, string departure_date, string departure_time, string departure_door, int statusD, int id_airline)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@id_airline", id_airline),
                new SqlParameter("@dep_date", departure_date),
                new SqlParameter("@dep_time", departure_time),
                new SqlParameter("@dep_door", departure_door),
                new SqlParameter("@dep_destination", destination),
                new SqlParameter("@id_status", statusD),
            };

            DatabaseHelper.DatabaseHelper.ExecStoreProcedure("sp_insert_flight_departure", param);

            return RedirectToAction("Index", "Flights");
        }

        private List<Flight> LoadFlightsArrivals()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_flights_arrivals", null);
            List<Flight> flightsListArrivals = new List<Flight>();

            foreach (DataRow row in ds.Rows)
            {
                flightsListArrivals.Add(new Flight()
                {
                    fli_number = Convert.ToInt16(row["fli_number"]),
                    air_name = row["air_name"].ToString(),
                    place = row["arr_origen"].ToString(),
                    date = row["arr_date"].ToString(),
                    time = row["arr_time"].ToString(),
                    door = row["arr_door"].ToString(),
                    description = row["sta_description"].ToString()
                });
            }

            return flightsListArrivals;
        }

        private List<Flight> LoadFlightsDepartures()
        {
            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("sp_select_flights_departures", null);
            List<Flight> flightsListDepartures = new List<Flight>();

            foreach (DataRow row in ds.Rows)
            {
                flightsListDepartures.Add(new Flight()
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

            return flightsListDepartures;
        }


        // GET: FlightsController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightsController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightsController1/Create
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

        // GET: FlightsController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightsController1/Edit/5
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

        // GET: FlightsController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightsController1/Delete/5
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
