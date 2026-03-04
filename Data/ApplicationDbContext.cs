using Microsoft.EntityFrameworkCore;
using DevPortfolio.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DevPortfolio.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    
    // These represent databse tables
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Service> Services { get; set; }

    // Add other DbSets as needed
}