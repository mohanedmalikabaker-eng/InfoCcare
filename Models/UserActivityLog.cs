using InfoCcare.Models;
namespace InfoCcare.Models.ViewModels
{
    public class UserActivityLog
    {
        public string? Email { get; set; }

        public string? Entity { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public List<AuditLog> Logs { get; set; } = new();
    }
}
