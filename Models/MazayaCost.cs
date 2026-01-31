using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCcare.Models
{
    public class MazayaCost
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal OfferPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal SimPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Cl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal PriceVatInclude { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
