using System.ComponentModel.DataAnnotations;

namespace InfoCcare.Models
{
    public class PrePaid_Data
    {
        public int Id { get; set; }
        public string Package { get; set; } = string.Empty;
        public int PriceNoVat { get; set; }
        public int PriceVat { get; set; }
        public string Code { get; set; } = string.Empty;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
