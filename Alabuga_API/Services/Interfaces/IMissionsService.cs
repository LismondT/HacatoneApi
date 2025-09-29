using Alabuga_API.Models;
using Alabuga_API.Persistens.Repositories.Interfaces;

namespace Alabuga_API.Services.Interfaces;

public interface IMissionsService
{
    Task<Mission?> GetMissionByIdAsync(int id);
    Task<IEnumerable<Mission>> GetAvailableMissionsAsync();
    Task<IEnumerable<Mission>> GetMissionsForUserAsync(int userId);
    Task<Mission> CreateMissionAsync(Mission mission);
    Task<Mission> UpdateMissionAsync(Mission mission);
    Task<bool> ArchiveMissionAsync(int id);
}