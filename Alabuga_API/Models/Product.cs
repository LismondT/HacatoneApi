using System;
using System.Collections.Generic;

namespace Alabuga_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? Description { get; set; }

    public string Price { get; set; } = null!;

    public int MaximumCountBuy { get; set; }

    public int FkRank { get; set; }

    public virtual Rank FkRankNavigation { get; set; } = null!;

    public virtual ICollection<UserPurchase> UserPurchases { get; set; } = new List<UserPurchase>();
}
