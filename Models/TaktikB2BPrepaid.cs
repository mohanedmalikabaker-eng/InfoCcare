using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class TaktikB2BPrepaid
    {
        public int Id { get; set; }
        public string Bundile { get; set; } = string.Empty;
        public string Internet { get; set; } = string.Empty;
        public string Units { get; set; } = string.Empty;
        public int PriceVatExluded { get; set; }
        public string FlexType { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
