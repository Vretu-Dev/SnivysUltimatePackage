using System.Collections.Generic;

namespace VVUP.HuskInfection
{
    public class CustomItemConfig
    {
        public List<HuskGrenade> HuskGrenades { get; set; } = new()
        {
            new HuskGrenade(),
        };

        public List<Calyxanide> Calyxanides { get; set; } = new()
        {
            new Calyxanide(),
        };
    }
}