namespace InfoCcare.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = null!;

        public string EntityName { get; set; } = null!;

        public string Action { get; set; } = null!;

        public DateTime TimesTamp { get; set; }

        public string Changes { get; set; } = null!;
    }
}
