namespace InfoCcare.Models
{
    public class RetailShop
    {

        public int Id { get; set; }
        public string FridayTime { get; set; } = string.Empty;
        public string SatToTheTime { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
