using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int FkCountry { get; set; }

    public virtual Role FkCountryNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
