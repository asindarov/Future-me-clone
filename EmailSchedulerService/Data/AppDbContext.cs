using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSchedulerService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }
    
    public DbSet<EmailDetail> EmailDetails { get; set; }
    
    public DbSet<Value> Values { get; set; }
}