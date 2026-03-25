using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppointmentMaker.Data
{
    public class ApplicationUser:IdentityUser
    {

        public string FullName { get; set; } = string.Empty;
       [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
