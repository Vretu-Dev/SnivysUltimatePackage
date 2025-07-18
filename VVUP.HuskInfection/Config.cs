using System.Collections.Generic;
using Exiled.API.Interfaces;
using VVUP.CustomRoles.Roles.Scps;

namespace VVUP.HuskInfection
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public static List<HuskZombie> HuskZombies { get; set; } = new()
        {
            new HuskZombie(),
        };

        public static List<HuskGrenade> HuskGrenades { get; set; } = new()
        {
            new HuskGrenade(),
        };

        public static List<Calyxanide> Calyxanides { get; set; } = new()
        {
            new Calyxanide(),
        };
    }
}