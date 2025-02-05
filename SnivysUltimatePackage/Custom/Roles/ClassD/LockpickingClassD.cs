using System.Collections.Generic;
using Exiled.API.Features.Spawn;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackage.API;
using SnivysUltimatePackage.Custom.Abilities.Active;

namespace SnivysUltimatePackage.Custom.Roles.ClassD
{
    public class LockpickingClassD : CustomRole, ICustomRole
    {
        public int Chance { get; set; } = 15;
        public StartTeam StartTeam { get; set; } = StartTeam.ClassD;
        public override uint Id { get; set; } = 46;
        public override int MaxHealth { get; set; } = 90;
        public override string Name { get; set; } = "<color=#FF8E00>Lock-picker Class D</color>";
        public override string Description { get; set; } = "Has the ability to lock pick doors";
        public override string CustomInfo { get; set; } = "Lock-picker Class D";
        public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;
        public override List<CustomAbility>? CustomAbilities { get; set; } = new()
        {
            new DoorPicking
            {
                Name = "Lock-picking Ability [Active]",
                Description = "Allows you to open any door for a short period of time, but limited by some external factors",
            }
        };
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
        };
    }
}