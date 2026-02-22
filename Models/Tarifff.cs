using System.ComponentModel.DataAnnotations.Schema;
namespace InfoCcare.Models
{
    public class Tarifff
    {
        public int Id { get; set; }
        public string CallSms { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,9)")]
        public decimal PriceVat { get; set; }
        [Column(TypeName = "decimal(18,9)")]
        public decimal PriceNoVat { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
