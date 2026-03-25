using DevPortfolio.Models;

namespace DevPortfolio.Models.ViewModels;

public class HomeViewModel
{
    public List<Skill> Skills { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    public List<Service> Services { get; set; } = new();

    // Optional: Add statistics
    public int TotalSkills => Skills.Count;
    public int TotalProjects => Projects.Count;
    public int TotalServices => Services.Count;

    // Grouped skills by category (for display)
    public IEnumerable<IGrouping<string, Skill>> SkillsByCategory =>
        Skills.GroupBy(s => s.Category ?? "Other").OrderBy(g => g.Key);
}