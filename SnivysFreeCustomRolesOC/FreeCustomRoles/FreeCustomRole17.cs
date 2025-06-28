using Exiled.API.Features.Attributes;
using Exiled.CustomRoles.API.Features;
using PlayerRoles;
using SnivysUltimatePackageOneConfig.API;

namespace SnivysFreeCustomRolesOC.FreeCustomRoles
{
    [CustomRole(RoleTypeId.None)]
    public class FreeCustomRole17 : CustomRole, ICustomRole
    {
        public int Chance { get; set; } = 0;
        public StartTeam StartTeam { get; set; } = StartTeam.Other;
        public override uint Id { get; set; } = 117;
        public override RoleTypeId Role { get; set; } = RoleTypeId.None;
        public override int MaxHealth { get; set; } = 100;
        public override string Name { get; set; } = "Free Custom Role 17";
        public override string Description { get; set; } = "Free Custom Role";
        public override string CustomInfo { get; set; } = "Free Custom Role";
    }
}