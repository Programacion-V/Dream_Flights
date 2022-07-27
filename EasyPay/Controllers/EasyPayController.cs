using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EasyPay.Controllers
{
    public class EasyPayController : Controller
    {
        // GET: EasyPayController1
        public ActionResult Index(int amount)
        {
            ViewBag.Amount = amount;

            return View();
        }

        public ActionResult Payment(string id)
        {
            ViewBag.Message = id;
            return View();
        }

        public ActionResult ExecutePay(string CardName,
                                       string CardNumber,
                                       string Month,
                                       string Year,
                                       string CVV,
                                       string Amount)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@num_card", CardNumber),
                new SqlParameter("@month_exp", Month),
                new SqlParameter("@year_exp", Year),
                new SqlParameter("@cvv", CVV),
                new SqlParameter("@amount", Amount)
            };

            DataTable ds = DatabaseHelper.DatabaseHelper.ExecuteStoreProcedure("ExecutePayment", param);

            if (ds.Rows.Count == 1)
            {
                var response = ds.Rows[0][0].ToString();
                
                if (response == "0")
                {
                    return RedirectToAction("Payment", "EasyPay", new { @id = "Payment Completed" });
                }
                if (response == "-4")
                {
                    return RedirectToAction("Payment", "EasyPay", new { @id = "Insufficient available funds" });
                }
            }

            return Ok();
        }
    }
}
