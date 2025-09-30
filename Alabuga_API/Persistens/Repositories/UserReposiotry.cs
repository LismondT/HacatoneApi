using Alabuga_API.Models.User;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alabuga_API.Persistens.Repositories;

public class UserReposiotry(AlabugaContext context) : IUserRepository
{
    public async Task<User?> GetRandom()
    {
        int count = await context.Users.AsNoTracking().CountAsync();

        if (count == 0)
            return null;

        Random random = new();
        int skip = random.Next(0, count);

        return await context.Users
            .AsNoTracking()
            .Include(u => u.FkRankNavigation)
            .Skip(skip)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await context.Users
            .Include(u => u.FkRankNavigation)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await context.Users
            .Include(u => u.FkRankNavigation)
            .FirstOrDefaultAsync(u => u.EMail == email);
    }
}