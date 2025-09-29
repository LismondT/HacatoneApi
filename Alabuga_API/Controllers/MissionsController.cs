using Alabuga_API.Contracts.Missions;
using Alabuga_API.Persistens.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alabuga_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MissionsController : ControllerBase
{
    private readonly IMissionsRepository _missionsRepository;
    private readonly ILogger<MissionsController> _logger;

    public MissionsController(
        IMissionsRepository missionsRepository,
        ILogger<MissionsController> logger)
    {
        _missionsRepository = missionsRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var missions = await _missionsRepository.GetAllAsync();
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting all missions");
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var mission = await _missionsRepository.GetByIdAsync(id);
            
            if (mission == null)
            {
                return NotFound($"Mission with ID {id} not found");
            }

            var missionData = ToMissionData(mission);
            
            var requirements = mission.MissionRequirements
                .Select(mr => new MissionRequirementData(
                    RankName: mr.FkRankNavigation?.Name ?? "Unknown",
                    MinimumExperience: mr.FkRankNavigation?.MinimumExpirience ?? 0
                ))
                .ToList();

            var response = new MissionDetailResponse(
                Mission: missionData,
                Requirements: requirements
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting mission with ID {MissionId}", id);
            return StatusCode(500, "An error occurred while retrieving the mission");
        }
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        try
        {
            var missions = await _missionsRepository.GetByCategoryAsync(categoryId);
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting missions for category {CategoryId}", categoryId);
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("branch/{branchId}")]
    public async Task<IActionResult> GetByBranch(int branchId)
    {
        try
        {
            var missions = await _missionsRepository.GetByBranchAsync(branchId);
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting missions for branch {BranchId}", branchId);
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("difficulty/{difficultyId}")]
    public async Task<IActionResult> GetByDifficulty(int difficultyId)
    {
        try
        {
            var missions = await _missionsRepository.GetByDifficultyAsync(difficultyId);
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting missions for difficulty {DifficultyId}", difficultyId);
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("online")]
    public async Task<IActionResult> GetOnlineMissions()
    {
        try
        {
            var missions = await _missionsRepository.GetOnlineMissionsAsync();
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting online missions");
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("with-artifacts")]
    public async Task<IActionResult> GetMissionsWithArtifacts()
    {
        try
        {
            var missions = await _missionsRepository.GetMissionsWithArtifactsAsync();
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting missions with artifacts");
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    [HttpGet("user/{userId}/available")]
    public async Task<IActionResult> GetAvailableMissionsForUser(int userId)
    {
        try
        {
            // In a real scenario, you'd get user's rank and skills from a user service
            // For now, let's assume we have a way to get this information
            var userRankId = 1; // This should come from user service
            var userSkillIds = new List<int> { 1, 2, 3 }; // This should come from user service

            var missions = await _missionsRepository.GetMissionsByRequirementsAsync(userRankId, userSkillIds);
            var missionData = missions.Select(ToMissionData).ToList();

            var response = new GetAllMissionsResponse(Missions: missionData);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting available missions for user {UserId}", userId);
            return StatusCode(500, "An error occurred while retrieving missions");
        }
    }

    private static MissionData ToMissionData(Models.Mission mission)
    {
        var artefacts = mission.ArtifactLoots
            .Select(al => al.FkArtifactNavigation)
            .Where(artifact => artifact != null)
            .Select(artifact => new ArtefactData(
                Id: artifact!.Id,
                Name: artifact.Name ?? "Unknown Artifact",
                Description: artifact.Description ?? string.Empty,
                Image: artifact.Image,
                RareName: artifact.FkRareNavigation?.Name ?? "Unknown",
                Lore: artifact.Lore
            ))
            .ToList();

        var skills = mission.SkillImprovements
            .Select(si => si.FkSkillNavigation)
            .Where(skill => skill != null)
            .Select(skill => new SkillData(
                Name: skill!.Name ?? "Unknown Skill",
                Description: skill.Description ?? string.Empty,
                Expirience: 10 // This should come from SkillImprovement or another source
            ))
            .ToList();

        return new MissionData(
            Id: mission.Id,
            Name: mission.Name ?? "Unknown Mission",
            Image: mission.Image,
            Description: mission.Description ?? string.Empty,
            Expirience: mission.Expirience,
            Energy: mission.Energy,
            Online: mission.Online,
            NeedFile: mission.NeedFile,
            BranchName: mission.FkBranchNavigation?.Name ?? "Unknown Branch",
            CategoryName: mission.FkCategoryNavigation?.Name ?? "Unknown Category",
            DifficultyName: mission.FkDifficultNavigation?.Name ?? "Unknown Difficulty",
            RankName: mission.FkRankNavigation?.Name ?? "Unknown Rank",
            Lore: mission.Lore,
            HasArtefactReward: artefacts.Any(),
            Artefacts: artefacts,
            Skills: skills
        );
    }
}