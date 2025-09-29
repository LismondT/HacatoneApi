using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Mission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public string? Description { get; set; }

    public int FkRank { get; set; }

    public int Expirience { get; set; }

    public int Energy { get; set; }

    public int FkCategory { get; set; }

    public bool Online { get; set; }

    public bool Relevant { get; set; }

    public bool NeedFile { get; set; }

    public int FkBranch { get; set; }

    public int FkDifficult { get; set; }

    public string? Lore { get; set; }

    public virtual ICollection<ArtifactLoot> ArtifactLoots { get; set; } = new List<ArtifactLoot>();

    public virtual Branch FkBranchNavigation { get; set; } = null!;

    public virtual Category FkCategoryNavigation { get; set; } = null!;

    public virtual Difficult FkDifficultNavigation { get; set; } = null!;

    public virtual Rank FkRankNavigation { get; set; } = null!;

    public virtual ICollection<MissionRequirement> MissionRequirements { get; set; } = new List<MissionRequirement>();

    public virtual ICollection<SkillImprovement> SkillImprovements { get; set; } = new List<SkillImprovement>();
}
