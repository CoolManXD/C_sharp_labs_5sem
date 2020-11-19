using System.Collections.Generic;

namespace DAL.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public List<ActiveOrder> ActiveOrders { get; set; }
        public Client()
        {
            ActiveOrders = new List<ActiveOrder>();
        }
    }
}
