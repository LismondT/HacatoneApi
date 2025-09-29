using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Alabuga_API.Models;

public partial class AlabugaContext : DbContext
{
    public AlabugaContext()
    {
    }

    public AlabugaContext(DbContextOptions<AlabugaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artifact> Artifacts { get; set; }

    public virtual DbSet<ArtifactLoot> ArtifactLoots { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Difficult> Difficults { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<MissionRequirement> MissionRequirements { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Rare> Rares { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillImprovement> SkillImprovements { get; set; }

    public virtual DbSet<SkillRequirement> SkillRequirements { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserArtifact> UserArtifacts { get; set; }

    public virtual DbSet<UserMission> UserMissions { get; set; }

    public virtual DbSet<UserPurchase> UserPurchases { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Alabuga;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artifact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Artifact_pkey");

            entity.ToTable("Artifact");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.FkRare).HasColumnName("FK_Rare");
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.Lore).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.FkRareNavigation).WithMany(p => p.Artifacts)
                .HasForeignKey(d => d.FkRare)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rare");
        });

        modelBuilder.Entity<ArtifactLoot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Artifact_loot_pkey");

            entity.ToTable("Artifact_loot");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkArtifact).HasColumnName("FK_Artifact");
            entity.Property(e => e.FkMission).HasColumnName("FK_Mission");

            entity.HasOne(d => d.FkArtifactNavigation).WithMany(p => p.ArtifactLoots)
                .HasForeignKey(d => d.FkArtifact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Artifact");

            entity.HasOne(d => d.FkMissionNavigation).WithMany(p => p.ArtifactLoots)
                .HasForeignKey(d => d.FkMission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Branch_pkey");

            entity.ToTable("Branch");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Country_pkey");

            entity.ToTable("Country");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Difficult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Difficult_pkey");

            entity.ToTable("Difficult");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Mission_pkey");

            entity.ToTable("Mission");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.FkBranch).HasColumnName("FK_Branch");
            entity.Property(e => e.FkCategory).HasColumnName("FK_Category");
            entity.Property(e => e.FkDifficult).HasColumnName("FK_Difficult");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.Lore).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.NeedFile).HasColumnName("Need_file");

            entity.HasOne(d => d.FkBranchNavigation).WithMany(p => p.Missions)
                .HasForeignKey(d => d.FkBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branch");

            entity.HasOne(d => d.FkCategoryNavigation).WithMany(p => p.Missions)
                .HasForeignKey(d => d.FkCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category");

            entity.HasOne(d => d.FkDifficultNavigation).WithMany(p => p.Missions)
                .HasForeignKey(d => d.FkDifficult)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Difficult");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.Missions)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rank");
        });

        modelBuilder.Entity<MissionRequirement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Mission_requirement_pkey");

            entity.ToTable("Mission_requirement");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkMission).HasColumnName("FK_Mission");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");

            entity.HasOne(d => d.FkMissionNavigation).WithMany(p => p.MissionRequirements)
                .HasForeignKey(d => d.FkMission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.MissionRequirements)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rank");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.MaximumCountBuy).HasColumnName("Maximum_count_buy");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Price).HasColumnType("character varying");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rank");
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rank_pkey");

            entity.ToTable("Rank");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");
            entity.Property(e => e.Image).HasColumnType("character varying");
            entity.Property(e => e.MinimumExpirience).HasColumnName("Minimum_expirience");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.InverseFkRankNavigation)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Rank");
        });

        modelBuilder.Entity<Rare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rare_pkey");

            entity.ToTable("Rare");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Region_pkey");

            entity.ToTable("Region");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkCountry).HasColumnName("FK_Country");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.FkCountryNavigation).WithMany(p => p.Regions)
                .HasForeignKey(d => d.FkCountry)
                .HasConstraintName("FK_Country");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasColumnType("character varying[]");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skill_pkey");

            entity.ToTable("Skill");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<SkillImprovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skill_improvement_pkey");

            entity.ToTable("Skill_improvement");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkMission).HasColumnName("FK_Mission");
            entity.Property(e => e.FkSkill).HasColumnName("FK_Skill");

            entity.HasOne(d => d.FkMissionNavigation).WithMany(p => p.SkillImprovements)
                .HasForeignKey(d => d.FkMission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission");

            entity.HasOne(d => d.FkSkillNavigation).WithMany(p => p.SkillImprovements)
                .HasForeignKey(d => d.FkSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill");
        });

        modelBuilder.Entity<SkillRequirement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skill_requirement_pkey");

            entity.ToTable("Skill_requirement");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");
            entity.Property(e => e.FkSkill).HasColumnName("FK_Skill");
            entity.Property(e => e.MinimumExpirience).HasColumnName("Minimum_expirience");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.SkillRequirements)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rank");

            entity.HasOne(d => d.FkSkillNavigation).WithMany(p => p.SkillRequirements)
                .HasForeignKey(d => d.FkSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.Direction).HasColumnType("character varying");
            entity.Property(e => e.EMail)
                .HasColumnType("character varying")
                .HasColumnName("E-mail");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("First_name");
            entity.Property(e => e.FkCountry).HasColumnName("FK_Country");
            entity.Property(e => e.FkRank).HasColumnName("FK_Rank");
            entity.Property(e => e.FkRegion).HasColumnName("FK_Region");
            entity.Property(e => e.FkRole).HasColumnName("FK_Role");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Patronymic).HasColumnType("character varying");
            entity.Property(e => e.Phone).HasColumnType("character varying");
            entity.Property(e => e.Photo).HasColumnType("character varying");
            entity.Property(e => e.Place).HasColumnType("character varying");
            entity.Property(e => e.Resume).HasColumnType("character varying");
            entity.Property(e => e.Sex).HasColumnType("character varying");

            entity.HasOne(d => d.FkCountryNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country");

            entity.HasOne(d => d.FkRankNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRank)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rank");

            entity.HasOne(d => d.FkRegionNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Region");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role");
        });

        modelBuilder.Entity<UserArtifact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_artifact_pkey");

            entity.ToTable("User_artifact");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkArtifact).HasColumnName("FK_Artifact");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkArtifactNavigation).WithMany(p => p.UserArtifacts)
                .HasForeignKey(d => d.FkArtifact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Artifact");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.UserArtifacts)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        modelBuilder.Entity<UserMission>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User_mission");

            entity.Property(e => e.File).HasColumnType("character varying");
            entity.Property(e => e.FkMission).HasColumnName("FK_Mission");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Result).HasColumnType("character varying");

            entity.HasOne(d => d.FkMissionNavigation).WithMany()
                .HasForeignKey(d => d.FkMission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mission");

            entity.HasOne(d => d.FkUserNavigation).WithMany()
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        modelBuilder.Entity<UserPurchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_purchases_pkey");

            entity.ToTable("User_purchases");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("ID");
            entity.Property(e => e.FkProduct).HasColumnName("FK_Product");
            entity.Property(e => e.FkUser).HasColumnName("FK_User");

            entity.HasOne(d => d.FkProductNavigation).WithMany(p => p.UserPurchases)
                .HasForeignKey(d => d.FkProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product");

            entity.HasOne(d => d.FkUserNavigation).WithMany(p => p.UserPurchases)
                .HasForeignKey(d => d.FkUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
