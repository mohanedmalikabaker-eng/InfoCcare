using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InfoCcare.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;

        [Required]
        public string Department { get; set; } = null!;


    }
}
