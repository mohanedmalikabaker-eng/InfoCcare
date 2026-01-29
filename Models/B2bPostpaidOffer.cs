using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCcare.Models
{
    public class B2bPostpaidOffer
    {
        public int Id { get; set; }
        public string Package { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal PriceNoVat { get; set; }
        public string Code { get; set; } = string.Empty;
        public string OffierType { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
