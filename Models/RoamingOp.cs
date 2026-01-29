namespace InfoCcare.Models
{
    public class RoamingOp
    {
        public int Id { get; set; }
        public string TapCode { get; set; } = string.Empty;
        public string RoamingParName { get; set; } = string.Empty;
        public string McCode { get; set; } = string.Empty;
        public string McNtCode { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public string RoamingServCode { get; set; } = string.Empty;
        public string StartDateOut { get; set; } = string.Empty;
        public string StartDateIn { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedById { get; set; } = string.Empty;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}
