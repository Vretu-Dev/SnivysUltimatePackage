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
        public Dictionary<DamageType, EventHandlers.HitBoxEnums> WeaponHitToEvaporate = new Dictionary<DamageType, EventHandlers.HitBoxEnums>
        {
            { DamageType.MicroHid, EventHandlers.HitBoxEnums.Body },
            { DamageType.MicroHid, EventHandlers.HitBoxEnums.Limb },
            { DamageType.MicroHid, EventHandlers.HitBoxEnums.Headshot },
            { DamageType.Revolver, EventHandlers.HitBoxEnums.Headshot },
        };
    }
}