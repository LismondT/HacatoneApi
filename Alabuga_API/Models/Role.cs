using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Role
{
    public int Id { get; set; }

    public List<string> Name { get; set; } = null!;

    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();

    public virtual ICollection<User.User> Users { get; set; } = new List<User.User>();
}
