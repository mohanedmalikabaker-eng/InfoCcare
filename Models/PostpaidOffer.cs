using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class PostpaidOffer
    {
        public int Id { get; set; }
        public string Package { get; set; } = string.Empty;
        public string Bouns { get; set; } = string.Empty;
        public string Mbs { get; set; } = string.Empty;
        public string MbsBouns { get; set; } = string.Empty;
        public string PriceNoVat { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string OffierType { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
