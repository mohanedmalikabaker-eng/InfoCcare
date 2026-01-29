using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public string CallSms { get; set; } = string.Empty;
        public int PriceVat { get; set; } 
        public int PriceNoVat { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
