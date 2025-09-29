using Alabuga_API.Models.User;

namespace Alabuga_API.Persistens.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetRandom();
}