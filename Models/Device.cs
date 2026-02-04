using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
namespace InfoCcare.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceType { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Bundle { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,9)")]
        public decimal EndPrice { get; set; }
        public string Img { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser? CreatedBy { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
