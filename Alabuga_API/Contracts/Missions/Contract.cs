using System.Text.Json.Serialization;

namespace Alabuga_API.Contracts.Missions;

// GET Responses
public record GetAllMissionsResponse(List<MissionData> Missions);
public record MissionDetailResponse(MissionData Mission, List<MissionRequirementData> Requirements);

// POST/PUT Requests
public record CreateMissionRequest(
    string Name,
    string? Image,
    string Description,
    int Expirience,
    int Energy,
    bool Online,
    bool NeedFile,
    int FkBranch,
    int FkCategory,
    int FkDifficult,
    int FkRank,
    string? Lore,
    List<int>? ArtefactIds,
    List<SkillImprovementRequest>? Skills
);

public record UpdateMissionRequest(
    string? Name,
    string? Image,
    string? Description,
    int? Expirience,
    int? Energy,
    bool? Online,
    bool? NeedFile,
    int? FkBranch,
    int? FkCategory,
    int? FkDifficult,
    int? FkRank,
    string? Lore,
    List<int>? ArtefactIds,
    List<SkillImprovementRequest>? Skills
);

public record SkillImprovementRequest(
    int SkillId,
    int ExpirienceGain
);

// Data models
public record MissionData(
    int Id,
    string Name,
    string? Image,
    string Description,
    int Expirience,
    int Energy,
    bool Online,
    bool NeedFile,
    string BranchName,
    string CategoryName,
    string DifficultyName,
    string RankName,
    string? Lore,
    bool HasArtefactReward,
    List<ArtefactData> Artefacts,
    List<SkillData> Skills
);

public record SkillData(
    string Name,
    string Description,
    int Expirience
);

public record ArtefactData(
    int Id,
    string Name,
    string Description,
    string? Image,
    string RareName,
    string? Lore
);

public record MissionRequirementData(
    string RankName,
    int MinimumExperience
);

public record CreateMissionResponse(int MissionId, string Message);
public record UpdateMissionResponse(string Message);
public record DeleteMissionResponse(string Message);