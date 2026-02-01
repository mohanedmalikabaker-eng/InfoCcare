namespace InfoCcare.Models
{
    public class Bade
    {
        public int Id { get; set; }
        public string Service { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
