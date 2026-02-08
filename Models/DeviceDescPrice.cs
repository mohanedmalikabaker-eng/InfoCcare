using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCcare.Models
{
    public class DeviceDescPrice
    {
        public int Id { get; set; }
        public string Bundle { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,9)")]
        public decimal PriceBe { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal PriceAf { get; set; } 
        public string Desc { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Note { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser? CreatedBy { get; set; }
    }
}
