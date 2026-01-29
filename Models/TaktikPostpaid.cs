using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class TaktikPostpaid
    {
        public int Id { get; set; }
        public string Bundles { get; set; } = string.Empty;
        public int Units { get; set; }
        public int PriceVatExluded { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }= DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
