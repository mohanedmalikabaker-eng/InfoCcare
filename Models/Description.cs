namespace InfoCcare.Models
{
    public class Description
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SegmentId { get; set; }
        public Segment Segment { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
