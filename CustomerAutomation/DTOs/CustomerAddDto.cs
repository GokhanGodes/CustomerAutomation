using System;
using System.Text.Json.Serialization;

namespace CustomerAutomation.DTOs
{
    public class CustomerAddDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string TCKN { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public bool IsActive { get; set; } = true;
    }
}
