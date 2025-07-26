using System.Collections.Generic;

namespace VVUP.HuskInfection
{
    public class CustomRoleConfig
    {
        public List<HuskZombie> HuskZombies { get; set; } = new()
        {
            new HuskZombie(),
        };
    }
}