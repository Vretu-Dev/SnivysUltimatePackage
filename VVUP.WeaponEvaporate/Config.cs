using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace VVUP.WeaponEvaporate
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        [Description("Types are 'Body', 'Headshot', and 'Limb' for HitboxTypes")]
        public Dictionary<DamageType, EventHandlers.HitBoxEnums> WeaponHitToEvaporate { get; set; } = new Dictionary<DamageType, EventHandlers.HitBoxEnums>
        {
            { DamageType.MicroHid, EventHandlers.HitBoxEnums.Body },
            { DamageType.Revolver, EventHandlers.HitBoxEnums.Headshot },
        };
    }
}