using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SkillImprovement> SkillImprovements { get; set; } = new List<SkillImprovement>();

    public virtual ICollection<SkillRequirement> SkillRequirements { get; set; } = new List<SkillRequirement>();
}
