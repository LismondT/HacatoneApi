using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User.User> Users { get; set; } = new List<User.User>();
}
