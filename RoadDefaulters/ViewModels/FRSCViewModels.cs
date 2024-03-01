namespace RoadDefaulters.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Penalty>? users { get; set; }
    }
    public class UserViewModel
    {
        public IEnumerable<Offence>? Offences { get; set; }
        public RoadUser? User { get; set; }
        public int OffenseID { get; set; }
        public string? Details { get; set; }
    }

}
