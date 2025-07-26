using System.Collections.Generic;
using Exiled.API.Interfaces;
using VVUP.CustomRoles.Roles.Scps;

namespace VVUP.HuskInfection
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public CustomItemConfig CustomItemConfig { get; set; } = new();
        public CustomRoleConfig CustomRoleConfig { get; set; } = new();
    }
}