using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class UserMission
{
    public int Id { get; set; }

    public int FkUser { get; set; }

    public int FkMission { get; set; }

    public DateOnly Date { get; set; }

    public string Result { get; set; } = null!;

    public string? File { get; set; }

    public bool Done { get; set; }

    public virtual Mission FkMissionNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
