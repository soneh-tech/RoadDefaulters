
namespace RoadDefaulters.Models
{
	public class AppUser : IdentityUser
	{
		public string? FullNames { get; set; }
	}
	public class AppUserDto
	{
		public string? FullNames { get; set; }
		public string? Email { get; set; }
		public string Password { get; set; }
	}
	public class RoadUser
	{
		public int RoadUserID { get; set; }
		public string FullName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string LicenseNumber { get; set; }
		public List<Penalty> Penalties { get; set; }
	}

	public class Penalty
	{
		public int PenaltyID { get; set; }
		public DateTime PenaltyDate { get; set; }
		public string Details { get; set; }
		public int OffenceID { get; set; }
		public int RoadUserID { get; set; }
		public Offence Offence { get; set; }
		public RoadUser User { get; set; }
	}
	public class Offence
	{
		public int OffenceID { get; set; }
		public string OffenceType { get; set; }
		public decimal OffenceAmount { get; set; }
	}

}
