using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class MissionRequirement
{
    public int Id { get; set; }

    public int FkRank { get; set; }

    public int FkMission { get; set; }

    public virtual Mission FkMissionNavigation { get; set; } = null!;

    public virtual Rank FkRankNavigation { get; set; } = null!;
}
