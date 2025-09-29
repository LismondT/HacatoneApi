using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class UserPurchase
{
    public int Id { get; set; }

    public int FkUser { get; set; }

    public int FkProduct { get; set; }

    public int Count { get; set; }

    public DateOnly Date { get; set; }

    public virtual Product FkProductNavigation { get; set; } = null!;

    public virtual User FkUserNavigation { get; set; } = null!;
}
