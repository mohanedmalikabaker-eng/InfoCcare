using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoCcare.Models
{
    public class Roaming
    {
        public int Id { get; set; }

        [Required]
        public string Zone { get; set; } = string.Empty;

        // ===== Prices =====

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Mb { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Sms { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Mtc { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Cbh { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Lcl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Inter { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Thuraya { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal Inmarsat { get; set; }

        // ===== Other =====

        public string Vat { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public string CreatedById { get; set; } = string.Empty;

        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
