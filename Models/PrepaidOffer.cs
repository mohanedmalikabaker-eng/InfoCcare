using System.ComponentModel.DataAnnotations;

namespace InfoCcare.Models
{
    public class PrepaidOffer
    {
        public int Id { get; set; }
        public string Packpage { get; set; } = string.Empty;
        public string PriceNoVat { get; set; } = string.Empty;
        public string PriceVat { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string OffierType { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
