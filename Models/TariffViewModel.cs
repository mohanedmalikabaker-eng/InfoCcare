namespace InfoCcare.Models
{
    public class TariffViewModel
    {
        public List<Tariff> Call { get; set; } = new();
        public List<Tariff> Sms { get; set; } = new();
    }
}
