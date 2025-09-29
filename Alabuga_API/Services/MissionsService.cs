using Alabuga_API.Models;
using Alabuga_API.Persistens;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Alabuga_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alabuga_API.Services;

public class MissionsService(IMissionsRepository missionsRepository, AlabugaContext context)
    : IMissionsService
{
    public async Task<Mission?> GetMissionByIdAsync(int id)
    {
        return await missionsRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Mission>> GetAvailableMissionsAsync()
    {
        return await missionsRepository.GetRelevantMissionsAsync();
    }

    public async Task<IEnumerable<Mission>> GetMissionsForUserAsync(int userId)
    {
        var user = await context.Users
            .Include(u => u.FkRankNavigation)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return Enumerable.Empty<Mission>();

        // Get user's skills from another repository or context
        var userSkills = await context.UserArtifacts
            .Where(ua => ua.FkUser == userId)
            .Select(ua => ua.FkArtifact)
            .ToListAsync();

        return await missionsRepository.GetMissionsByRequirementsAsync(
            user.FkRank, 
            userSkills);
    }

    public async Task<Mission> CreateMissionAsync(Mission mission)
    {
        return await missionsRepository.CreateAsync(mission);
    }

    public async Task<Mission> UpdateMissionAsync(Mission mission)
    {
        return await missionsRepository.UpdateAsync(mission);
    }

    public async Task<bool> ArchiveMissionAsync(int id)
    {
        return await missionsRepository.DeleteAsync(id);
    }
}