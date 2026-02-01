using System.ComponentModel.DataAnnotations;
namespace InfoCcare.Models
{
    public class BadeFees
    {
        public int Id { get; set; }
        public string Star { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
        public string Avg { get; set; } = string.Empty;
        public string Fees { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
