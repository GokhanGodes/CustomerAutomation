using System;

namespace CustomerAutomation.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string TCKN { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
