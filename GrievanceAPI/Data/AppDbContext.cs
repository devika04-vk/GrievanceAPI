using Microsoft.EntityFrameworkCore;
using GrievanceAPI.Models;

namespace GrievanceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ResolutionHistory> ResolutionHistories { get; set; }
    }
}