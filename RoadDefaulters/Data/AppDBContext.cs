
namespace RoadDefaulters.Data
{
    public class AppDBContext : IdentityDbContext<AppUser>
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{
		}
		public DbSet<RoadUser> Users { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<Offence> Offences { get; set; }
    }
}
