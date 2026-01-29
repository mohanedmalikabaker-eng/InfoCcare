namespace InfoCcare.Models
{
    public class Segment
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string CreatedById { get; set; } = string.Empty;

        public ApplicationUser CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
