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
            .Skip(skip)
            .FirstOrDefaultAsync();
    }
}