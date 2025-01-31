using Microsoft.EntityFrameworkCore;
using Preqin.InvestorsApi.Models;

namespace Preqin.InvestorsApi.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Commitment> Commitments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investor>()
                .ToTable("investors")
                .HasKey(i => i.InvestorId);

            modelBuilder.Entity<Commitment>()
                .ToTable("commitments")
                .HasKey(c => c.CommitmentId);

            modelBuilder.Entity<Commitment>()
                .HasOne(c => c.Investor)
                .WithMany(i => i.Commitments)
                .HasForeignKey(c => c.InvestorId);
        }

}