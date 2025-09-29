public record GetAllMissionsResponse(
    List<MissionData> Missions
);

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