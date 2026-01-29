namespace InfoCcare.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string RomingPartnerName { get; set; } = string.Empty;
        public string ZoneNo { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
