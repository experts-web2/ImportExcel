using Microsoft.EntityFrameworkCore;

namespace ImportExcel.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Order> Orders { get; set; }

	}
}
