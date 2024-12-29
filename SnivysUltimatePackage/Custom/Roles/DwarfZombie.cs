using System.Collections.Generic;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackage.API;
using SnivysUltimatePackage.Custom.Abilities;

namespace SnivysUltimatePackage.Custom.Roles
{
    public class DwarfZombie : CustomRole
    {
        public int Chance { get; set; } = 20;
        public override uint Id { get; set; } = 43;
        public override int MaxHealth { get; set; } = 200;
        public override string Name { get; set; } = "<color=#FF0000>Dwarf SCP-049-2</color>";
        public override string Description { get; set; } = "A smaller zombie";
        public override string CustomInfo { get; set; } = "<color=#FF0000>Dwarf SCP-049-2</color>";
        public override RoleTypeId Role { get; set; } = RoleTypeId.Scp0492;
        
        public StartTeam StartTeam { get; set; } = StartTeam.Revived;

        public override List<CustomAbility> CustomAbilities { get; set; } = new List<CustomAbility>
        {
            new DwarfAbility
            {
                Name = "Dwarf [Passive]",
                Description = "Makes you small as a dwarf zombie",
            },
        };
    }
}