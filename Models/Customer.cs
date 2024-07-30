using Dapper.Contrib.Extensions;

namespace WebApiAuthetication.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }

        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}