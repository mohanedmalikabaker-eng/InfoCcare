using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class MazayaExtraUnits
    {
        public int Id { get; set; }
        public string ExtraUnits { get; set; } = string.Empty;
        public string PriceVatEx { get; set; } = string.Empty;
        public string PriceVatIn { get; set; } = string.Empty;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
