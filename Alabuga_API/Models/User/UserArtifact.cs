namespace Alabuga_API.Models.User;

public partial class UserArtifact
{
    public int Id { get; set; }

    public int FkUser { get; set; }

    public int FkArtifact { get; set; }

    public virtual Artifact FkArtifactNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
