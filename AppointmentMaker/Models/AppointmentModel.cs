using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentMaker.Models
{
    public class AppointmentModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //Will auto increment ID number in the list

        [Required] // patient's name strictly required
        [StringLength(20, MinimumLength = 4)]  // data validation, the field will have max characters of 20 and min of 4
        [DisplayName("Patient Full Name")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [DisplayName("Appointment Date")] // This display on top of each input as a label
        public DateTime Date { get; set; }

        [Required] // patient's name strictly required
        [StringLength(20, MinimumLength = 4)]  // data validation, the field will have max characters of 20 and min of 4
        [DisplayName("Doctor Last Name")]
        public string DoctorName { get; set; } = string.Empty;

        // [DataType(DataType.Currency)] // Field for currency
        [DisplayName("Doctor Room")] 
        public int DoctorRoom { get; set; }

        [Range(1,10)] // fiels for range between 1 to 10
        [DisplayName("Patient Pain Level (1 low - 10 high)")]
        public int PainLevel { get; set; }
    }
}
