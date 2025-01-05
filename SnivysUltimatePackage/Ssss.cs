using System.Collections.Generic;
using System.Text;
using Exiled.CustomRoles.API.Features;
using NorthwoodLib.Pools;
using SnivysUltimatePackage.Custom.Abilities;
using SnivysUltimatePackage.Custom.Roles;
using UnityEngine;
using UserSettings.ServerSpecific;
using Flipped = SnivysUltimatePackage.Custom.Abilities.Flipped;

namespace SnivysUltimatePackage
{
    public class Ssss
    {
        public static ServerSpecificSettingBase[] VVUltimatePluginPackage()
        {
            return SsssHelper.GetSettings();
        }
    }

    public class SsssHelper
    {
        public static ServerSpecificSettingBase[] GetSettings()
        {
            List<ServerSpecificSettingBase> settings = new List<ServerSpecificSettingBase>();
            StringBuilder customRoleStringBuilder = StringBuilderPool.Shared.Rent();

            var customRoles = new List<CustomRole>
            {
                Biochemist.Get(typeof(Biochemist)),
                CiPhantom.Get(typeof(CiPhantom)),
                CISpy.Get(typeof(CISpy)),
                DwarfZombie.Get(typeof(DwarfZombie)),
                ExplosiveZombie.Get(typeof(ExplosiveZombie)),
                Custom.Roles.Flipped.Get(typeof(Flipped)),
                JuggernautChaos.Get(typeof(JuggernautChaos)),
                LockpickingClassD.Get(typeof(LockpickingClassD)),
                MedicZombie.Get(typeof(MedicZombie)),
                MtfWisp.Get(typeof(MtfWisp)),
                Nightfall.Get(typeof(Nightfall)),
                TelepathicChaos.Get(typeof(TelepathicChaos)),
            };

            foreach (var role in customRoles)
            {
                if (role == null || role.CustomAbilities == null) continue;

                customRoleStringBuilder.AppendLine($"Role: {role.Name}");
                customRoleStringBuilder.AppendLine($"- Description: {role.Description}");
                foreach (var ability in role.CustomAbilities)
                {
                    customRoleStringBuilder.AppendLine($"-- Ability: {ability.Name}, {ability.Description}");
                }
            }

            // Add the collected abilities to the settings
            settings.Add(new SSGroupHeader("VV Custom Roles"));
            settings.Add(new SSTextArea(null, StringBuilderPool.Shared.ToStringReturn(customRoleStringBuilder), SSTextArea.FoldoutMode.ExtendedByDefault));

            settings.Add(new SSGroupHeader("VV Custom Abilities Key Activators"));
            settings.Add(new SSKeybindSetting(10000, "Active Camo", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(10001, "Charge", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(10002, "Detect", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(10003, "Door Picking", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(10004, "Healing Mist", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(10005, "Remove Disguise", KeyCode.B, true, null));
            
            return settings.ToArray();
        }
    }

}