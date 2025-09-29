using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class User
{
    public int Id { get; set; }

    public string EMail { get; set; } = null!;

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly Birthdate { get; set; }

    public string Sex { get; set; } = null!;

    public int FkCountry { get; set; }

    public string Place { get; set; } = null!;

    public string? Photo { get; set; }

    public string? Resume { get; set; }

    public int? Energy { get; set; }

    public int? Expirience { get; set; }

    public int FkRank { get; set; }

    public string? Direction { get; set; }

    public int FkRole { get; set; }

    public int FkRegion { get; set; }

    public virtual Country FkCountryNavigation { get; set; } = null!;

    public virtual Rank FkRankNavigation { get; set; } = null!;

    public virtual Region FkRegionNavigation { get; set; } = null!;

    public virtual Role FkRoleNavigation { get; set; } = null!;

    public virtual ICollection<UserArtifact> UserArtifacts { get; set; } = new List<UserArtifact>();

    public virtual ICollection<UserPurchase> UserPurchases { get; set; } = new List<UserPurchase>();
}
