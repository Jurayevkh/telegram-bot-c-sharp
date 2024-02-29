using Microsoft.EntityFrameworkCore;
using Bot.Entity;
using Bot.Configuration;

namespace Bot.Data;

public class BotDbContext : DbContext
{
	public BotDbContext(DbContextOptions<BotDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
