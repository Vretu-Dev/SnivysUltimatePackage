using System.Collections.Generic;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackageOneConfig.API;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Active;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Passive;

namespace SnivysUltimatePackageOneConfig.Custom.Roles.Chaos
{
    [CustomRole(RoleTypeId.ChaosConscript)]
    public class CISpy : CustomRole, ICustomRole
    {
        public int Chance { get; set; } = 0;

        public StartTeam StartTeam { get; set; } = StartTeam.Ntf;

        public override uint Id { get; set; } = 40;

        public override RoleTypeId Role { get; set; } = RoleTypeId.NtfSergeant;

        public override int MaxHealth { get; set; } = 100;

        public override string Name { get; set; } = "Chaos Insurgency Spy";

        public override string Description { get; set; } = "A Chaos Insurgent that is disguised as a MTF Member";

        public override string CustomInfo { get; set; } = "NTF Sergeant";

        public override bool KeepInventoryOnSpawn { get; set; } = true;

        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            RoleSpawnPoints = new List<RoleSpawnPoint>
            {
                new()
                {
                    Role = RoleTypeId.NtfSpecialist,
                    Chance = 100,
                },
            },
        };

        public override List<CustomAbility>? CustomAbilities { get; set; } = new()
        {
            new Disguised
            {
                Name = "Disguised [Passive]",
                Description = "Handles everything related to being disguised",
                DisguisedCi = "That MTF is actually on the CI side",
                DisguisedMtf = "",
                DisguisedHintDisplay = true,
                DisguisedTextDisplayTime = 5,
            },
            new RemoveDisguise
            {
                Name = "Remove Disguise [Active]",
                Description =
                    "This removes your disguise, once it''s off, you cannot put it back on, activate carefully",
                Duration = 0,
                Cooldown = 5,
                RestorePreviousInventory = false,
            },
        };
    }
}