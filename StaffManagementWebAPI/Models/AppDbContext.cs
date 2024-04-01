using Microsoft.EntityFrameworkCore;

namespace StaffManagementWebAPI.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Staff> Staff { get; set; }
	}
}
