using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class SkillImprovement
{
    public int Id { get; set; }

    public int FkMission { get; set; }

    public int FkSkill { get; set; }

    public int Expirience { get; set; }

    public virtual Mission FkMissionNavigation { get; set; } = null!;

    public virtual Skill FkSkillNavigation { get; set; } = null!;
}
