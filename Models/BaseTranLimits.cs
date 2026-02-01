using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class BaseTranLimits
    {
        public int Id { get; set; }
        public string TransactionLimits { get; set; } = string.Empty;
        public string Normal { get; set; } = string.Empty;
        public string Silver { get; set; } = string.Empty;
        public string Golden { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
