using System;
using System.Collections.Generic;
using Alabuga_API.Models.User;

namespace Alabuga_API.Models;

public partial class Artifact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? Description { get; set; }

    public int FkRare { get; set; }

    public string? Lore { get; set; }

    public virtual ICollection<ArtifactLoot> ArtifactLoots { get; set; } = new List<ArtifactLoot>();

    public virtual Rare FkRareNavigation { get; set; } = null!;

    public virtual ICollection<UserArtifact> UserArtifacts { get; set; } = new List<UserArtifact>();
}
