using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackageOneConfig.API;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Active;

namespace SnivysUltimatePackageOneConfig.Custom.Roles.ClassD;

public class ClassDAnalyst : CustomRole, ICustomRole
{
    public int Chance { get; set; } = 15;
    public StartTeam StartTeam { get; set; } = StartTeam.ClassD;
    public override uint Id { get; set; } = 51;
    public override int MaxHealth { get; set; } = 100;
    public override string Name { get; set; } = "<color=#FF8E00>Class-D Analyst</color>";

    public override string Description { get; set; } =
        "A Class-D with a knack for analyzing and gathering information.";

    public override string CustomInfo { get; set; } = "Class-D Analyst";
    
    public override List<CustomAbility>? CustomAbilities { get; set; } = new()
    {
        new Detect
        {
            Name = "Detect [Active]",
            Description = "Detects targets for you, a Class-D.",
            Duration = 0,
            Cooldown = 120,
            DetectRange = 30,
            ShowMissingRoles = true,
            RoleNames = new Dictionary<RoleTypeId, string>()
            {
                {RoleTypeId.Scp049, "SCP-049"},
                {RoleTypeId.Scp0492, "SCP-049-2"},
                {RoleTypeId.Scp079, "SCP-079"},
                {RoleTypeId.Scp096, "SCP-096"},
                {RoleTypeId.Scp106, "SCP-106"},
                {RoleTypeId.Scp173, "SCP-173"},
                {RoleTypeId.Scp939, "SCP-939"},
            },
        },
    };
    public override SpawnProperties SpawnProperties { get; set; } = new()
    {
        Limit = 1,
    };
}