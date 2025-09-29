using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Rank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int MinimumExpirience { get; set; }

    public int? FkRank { get; set; }

    public virtual Rank? FkRankNavigation { get; set; }

    public virtual ICollection<Rank> InverseFkRankNavigation { get; set; } = new List<Rank>();

    public virtual ICollection<MissionRequirement> MissionRequirements { get; set; } = new List<MissionRequirement>();

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<SkillRequirement> SkillRequirements { get; set; } = new List<SkillRequirement>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
