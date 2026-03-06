using Microsoft.EntityFrameworkCore;
using DevPortfolio.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Win32.SafeHandles;

namespace DevPortfolio.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed Skills
        modelBuilder.Entity<Skill>().HasData
        (
            new Skill
            {
                Id = 1,
                Name = "C#",
                Level = 85,
                Category = "Backend"
            },
            new Skill
            {
                Id = 2,
                Name = "Javascript",
                Level = 70,
                Category = "Froentend",
            },
            new Skill
            {
                Id = 3,
                Name = "SQL",
                Level = 60,
                Category = "Database",
            },
            new Skill
            {
                Id = 4,
                Name = "ASP.Net Core",
                Level = 75,
                Category = "Backend"
            }
        );

        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Title = "Portfolio Website",
                Description = "<y personal portfolio built with ASP.NET Core MVC.",
                GitHubUrl = "https://github.com/KMahlangu/DevPortfolio",
                Technologies = "C#, ASP.NET Core, Bootstrap",
            },
            new Project
            {
            Id = 2,
            Title = "Task Manager App",
            Description = "A simple task management application",
            LiveUrl = "https://example.com",
            Technologies = "JavaScript, Node.js, MongoDB",
            }

        );

        // Seed Projects
        modelBuilder.Entity<Service>().HasData
        (
            new Service
            {
                Id = 1,
                Title = "Web Development",
                Description = "Custom web applications built with ASP.NET Core",
                IconClass = "bi-code-slash"
            },
            new Service
            {
                Id = 2,
                Title = "Database Design",
                Description = "Effecient database architecture and optimization",
                IconClass = "bi-database",
            },
            new Service
            {
                Id = 3,
                Title = "API Development",
                Description = "RESTful APIs for seamless integration",
                IconClass = "bi-plugin",
            }
        );

        // Seed Certificates Data
        modelBuilder.Entity<Certificate>(entity =>
       {
           entity.HasKey(c => c.Id);
           entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
           entity.Property(c => c.IssuingOrganization).IsRequired().HasMaxLength(200);
           // Add other configurations as needed
       });
    }
    
    // These represent databse tables
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
}