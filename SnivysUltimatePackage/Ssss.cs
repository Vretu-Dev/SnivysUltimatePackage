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
                A7Chaos.Get(typeof(A7Chaos)),
                Biochemist.Get(typeof(Biochemist)),
                BorderPatrol.Get(typeof(BorderPatrol)),
                CiPhantom.Get(typeof(CiPhantom)),
                CISpy.Get(typeof(CISpy)),
                ContainmentGuard.Get(typeof(ContainmentGuard)),
                ContainmentScientist.Get(typeof(ContainmentScientist)),
                Demolitionist.Get(typeof(Demolitionist)),
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
            settings.Add(new SSGroupHeader("VV Custom Roles Info"));
            settings.Add(new SSTextArea(null, StringBuilderPool.Shared.ToStringReturn(customRoleStringBuilder), SSTextArea.FoldoutMode.CollapsedByDefault));

            settings.Add(new SSGroupHeader("VV Custom Abilities Key Activators"));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.ActiveCamoId, "Active Camo", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.ChargeId, "Charge", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.DetectId, "Detect", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.DoorPickingId, "Door Picking", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.HealingMistId, "Healing Mist", KeyCode.B, true, null));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.RemoveDisguiseId, "Remove Disguise", KeyCode.B, true, null));
            
            settings.Add(new SSGroupHeader("VV Custom Items Activators"));
            settings.Add(new SSKeybindSetting(Plugin.Instance.Config.SsssConfig.DetonateC4Id, "Detonate C4", KeyCode.J, true, null));
            
            return settings.ToArray();
        }
    }

}