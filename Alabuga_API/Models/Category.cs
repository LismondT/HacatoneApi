using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Mission> Missions { get; set; } = new List<Mission>();
}
