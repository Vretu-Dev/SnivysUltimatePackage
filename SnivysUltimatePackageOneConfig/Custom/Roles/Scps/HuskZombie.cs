using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackageOneConfig.API;
using SnivysUltimatePackageOneConfig.Custom.Abilities.Passive;
using UnityEngine;

namespace SnivysUltimatePackageOneConfig.Custom.Roles.Scps
{
    public class HuskZombie : CustomRole, ICustomRole
    {
        public int Chance { get; set; } = 0;
        public override uint Id { get; set; } = 56;
        public override int MaxHealth { get; set; } = 350;
        public override string Name { get; set; } = "<color=#FF0000>Husk</color>";
        public override string Description { get; set; } = "Unlike other zombies, this is a Husk, came from someone who was infected something giving a living player a growing husk inside of them, which then took over.";
        public override string CustomInfo { get; set; } = "Husk";
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;
        
        public StartTeam StartTeam { get; set; } = StartTeam.Scp | StartTeam.Revived;

        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
           new ApplyHuskInfection()
           {
               Name = "Apply Husk Infection On Hit [Passive]",
           },
        };
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 0,
        };
    }
}