namespace InfoCcare.Models
{
    public class Dealer
    {
        public int Id { get; set; }
        public string DealerName { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string CityLocation { get; set; } = string.Empty;
        public string AgentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string SimSwap { get; set; } = string.Empty;
        public string SimSales { get; set; } = string.Empty;
        public string AirtimeSales { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
