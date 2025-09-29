using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class SkillRequirement
{
    public int Id { get; set; }

    public int FkRank { get; set; }

    public int FkSkill { get; set; }

    public int MinimumExpirience { get; set; }

    public virtual Rank FkRankNavigation { get; set; } = null!;

    public virtual Skill FkSkillNavigation { get; set; } = null!;
}
