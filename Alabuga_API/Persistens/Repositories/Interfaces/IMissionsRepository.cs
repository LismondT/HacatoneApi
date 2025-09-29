using Alabuga_API.Models;

namespace Alabuga_API.Persistens.Repositories.Interfaces;

public interface IMissionsRepository
{
    Task<IEnumerable<Mission>> GetMissionsWithDetailsAsync();
    Task<Mission?> GetByIdAsync(int id);
    Task<IEnumerable<Mission>> GetAllAsync();
    Task<IEnumerable<Mission>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<Mission>> GetByBranchAsync(int branchId);
    Task<IEnumerable<Mission>> GetByDifficultyAsync(int difficultyId);
    Task<IEnumerable<Mission>> GetByRankAsync(int rankId);
    Task<IEnumerable<Mission>> GetOnlineMissionsAsync();
    Task<IEnumerable<Mission>> GetRelevantMissionsAsync();
    Task<IEnumerable<Mission>> GetMissionsWithArtifactsAsync();
    Task<Mission> CreateAsync(Mission mission);
    Task<Mission> UpdateAsync(Mission mission);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Mission>> GetMissionsByRequirementsAsync(int userRankId, IEnumerable<int> userSkillIds);
}