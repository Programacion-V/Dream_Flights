namespace Dream_Flights.Models
{
    public class PurchaseHistory
    {
        public int id_flight { get; set; }
        public string air_name { get; set; }
        public string fulldate { get; set; }
        public string dep_destination { get; set; }
        public int ez_transaction { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string amount { get; set; }
        public string status_description { get; set; }

    }
}
