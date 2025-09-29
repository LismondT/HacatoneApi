using Alabuga_API.Models;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alabuga_API.Persistens.Repositories;

public class MissionsRepository(AlabugaContext context) : IMissionsRepository
{
    public async Task<IEnumerable<Mission>> GetMissionsWithDetailsAsync()
    {
        return await context.Missions
            .Include(m => m.FkBranchNavigation)
            .Include(m => m.FkCategoryNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Include(m => m.ArtifactLoots)
            .ThenInclude(al => al.FkArtifactNavigation)
            .ThenInclude(a => a.FkRareNavigation)
            .Include(m => m.SkillImprovements)
            .ThenInclude(si => si.FkSkillNavigation)
            .Include(m => m.MissionRequirements)
            .ThenInclude(mr => mr.FkRankNavigation)
            .Where(m => m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ThenBy(m => m.FkDifficultNavigation.Id)
            .ToListAsync();
    }

    public async Task<Mission?> GetByIdAsync(int id)
    {
        return await context.Missions
            .Include(m => m.FkBranchNavigation)
            .Include(m => m.FkCategoryNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Include(m => m.ArtifactLoots)
            .ThenInclude(al => al.FkArtifactNavigation)
            .Include(m => m.SkillImprovements)
            .ThenInclude(si => si.FkSkillNavigation)
            .Include(m => m.MissionRequirements)
            .ThenInclude(mr => mr.FkRankNavigation)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Mission>> GetAllAsync()
    {
        return await GetMissionsWithDetailsAsync();
    }

    public async Task<IEnumerable<Mission>> GetByCategoryAsync(int categoryId)
    {
        return await context.Missions
            .Include(m => m.FkCategoryNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.FkCategory == categoryId && m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetByBranchAsync(int branchId)
    {
        return await context.Missions
            .Include(m => m.FkBranchNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.FkBranch == branchId && m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetByDifficultyAsync(int difficultyId)
    {
        return await context.Missions
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.FkDifficult == difficultyId && m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetByRankAsync(int rankId)
    {
        return await context.Missions
            .Include(m => m.FkRankNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Where(m => m.FkRank == rankId && m.Relevant)
            .OrderBy(m => m.FkDifficultNavigation.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetOnlineMissionsAsync()
    {
        return await context.Missions
            .Include(m => m.FkCategoryNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.Online && m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetRelevantMissionsAsync()
    {
        return await context.Missions
            .Include(m => m.FkBranchNavigation)
            .Include(m => m.FkCategoryNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ThenBy(m => m.FkDifficultNavigation.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetMissionsWithArtifactsAsync()
    {
        return await context.Missions
            .Include(m => m.ArtifactLoots)
            .ThenInclude(al => al.FkArtifactNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.FkRankNavigation)
            .Where(m => m.ArtifactLoots.Any() && m.Relevant)
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ToListAsync();
    }

    public async Task<IEnumerable<Mission>> GetMissionsByRequirementsAsync(int userRankId,
        IEnumerable<int> userSkillIds)
    {
        var userRank = await context.Ranks.FindAsync(userRankId);
        if (userRank == null)
            return Enumerable.Empty<Mission>();

        return await context.Missions
            .Include(m => m.FkRankNavigation)
            .Include(m => m.FkDifficultNavigation)
            .Include(m => m.MissionRequirements)
            .Include(m => m.SkillImprovements)
            .Where(m => m.Relevant &&
                        m.FkRankNavigation.MinimumExpirience <= userRank.MinimumExpirience &&
                        (!m.MissionRequirements.Any() ||
                         m.MissionRequirements.All(mr =>
                             mr.FkRankNavigation.MinimumExpirience <= userRank.MinimumExpirience)))
            .OrderBy(m => m.FkRankNavigation.MinimumExpirience)
            .ThenBy(m => m.FkDifficultNavigation.Id)
            .ToListAsync();
    }

    public async Task<Mission> CreateAsync(Mission mission)
    {
        context.Missions.Add(mission);
        await context.SaveChangesAsync();
        return mission;
    }

    public async Task<Mission> UpdateAsync(Mission mission)
    {
        context.Missions.Update(mission);
        await context.SaveChangesAsync();
        return mission;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var mission = await context.Missions.FindAsync(id);
        if (mission == null)
            return false;

        mission.Relevant = false;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Missions.AnyAsync(m => m.Id == id);
    }
}