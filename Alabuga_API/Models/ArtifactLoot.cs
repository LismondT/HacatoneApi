using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class ArtifactLoot
{
    public int Id { get; set; }

    public int FkArtifact { get; set; }

    public int FkMission { get; set; }

    public virtual Artifact FkArtifactNavigation { get; set; } = null!;

    public virtual Mission FkMissionNavigation { get; set; } = null!;
}
