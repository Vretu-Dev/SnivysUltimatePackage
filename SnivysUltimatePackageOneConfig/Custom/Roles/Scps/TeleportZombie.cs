using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackageOneConfig.API;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Active;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Passive;

namespace SnivysUltimatePackageOneConfig.Custom.Roles.Scps
{
    public class TeleportZombie : CustomRole, ICustomRole
    {
        public int Chance { get; set; } = 10;
        public override uint Id { get; set; } = 57;
        public override int MaxHealth { get; set; } = 300;
        public override string Name { get; set; } = "<color=#FF0000>Teleport Zombie</color>";
        public override string Description { get; set; } = "A zombie that can teleport";
        public override string CustomInfo { get; set; } = "Teleport Zombie";
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;
        
        public StartTeam StartTeam { get; set; } = StartTeam.Scp | StartTeam.Revived;

        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
            new EffectEnabler()
            {
                Name = "Speed Boost [Passive]",
                Description = "Increases your movement speed",
                EffectsToApply = new Dictionary<EffectType, byte>()
                {
                    { EffectType.Slowness, 20 },
                },
            },
            new Teleport
            {
                Name = "Teleport [Active]",
                Description = "Teleports to a random location within a certain range",
            },
        };
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 2,
        };
        
        public override string AbilityUsage { get; set; } = "Use your Noclip Button [Left Alt] to swap abilities and to activate. Tap Twice to Swap. Tap Once to activate.";
    }
}