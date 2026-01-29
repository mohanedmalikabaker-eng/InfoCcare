using System.ComponentModel.DataAnnotations;

namespace InfoCcare.Models
{
    public class TaktikPrepaid
    {
        public int Id { get; set; }
        public string Bundile { get; set; } = string.Empty;
        public string Units { get; set; } = string.Empty;
        public int PriceVatExluded { get; set; }
        public int PriceVatIncluded { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FlexType { get; set; } = string.Empty;
        public string NormalGift { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;

    }
}
