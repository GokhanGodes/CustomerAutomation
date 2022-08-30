using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomerAutomationWeb.Models
{
    public class CustomerAddInput
    {
        [JsonIgnore]

        public int Id { get; set; }

        [Required]
        [DisplayName("Kimlik No")]
        public string TCKN { get; set; }

        [Required]
        [DisplayName("Ad")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyad")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; } = true;
    }
}
