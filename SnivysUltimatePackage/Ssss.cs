using System.Collections.Generic;
using System.Text;
using Exiled.API.Features.Items;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API.Features;
using NorthwoodLib.Pools;
using SnivysUltimatePackage.Custom.Items.Armor;
using SnivysUltimatePackage.Custom.Items.Firearms;
using SnivysUltimatePackage.Custom.Items.Grenades;
using SnivysUltimatePackage.Custom.Items.Injections;
using SnivysUltimatePackage.Custom.Items.Other;
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

            settings.Add(new SSGroupHeader("VV Custom Roles Info"));
            settings.Add(new SSTextArea(null, StringBuilderPool.Shared.ToStringReturn(customRoleStringBuilder), SSTextArea.FoldoutMode.CollapsedByDefault));

            StringBuilder customItemStringBuilder = StringBuilderPool.Shared.Rent();
            var customItems = new List<CustomItem>
            {
                (CustomItem)ExplosiveResistantArmor.Get(typeof(ExplosiveResistantArmor)),
                (CustomItem)ExplosiveRoundRevolver.Get(typeof(ExplosiveRoundRevolver)),
                (CustomItem)MediGun.Get(typeof(MediGun)),
                (CustomItem)Tranquilizer.Get(typeof(Tranquilizer)),
                (CustomItem)C4.Get(typeof(C4)),
                (CustomItem)EmpGrenade.Get(typeof(EmpGrenade)),
                (CustomItem)NerveAgentGrenade.Get(typeof(NerveAgentGrenade)),
                (CustomItem)SmokeGrenade.Get(typeof(SmokeGrenade)),
                (CustomItem)DeadringerSyringe.Get(typeof(DeadringerSyringe)),
                (CustomItem)KySyringe.Get(typeof(KySyringe)),
                (CustomItem)AntiScp096Pills.Get(typeof(AntiScp096Pills)),
                (CustomItem)InfinitePills.Get(typeof(InfinitePills)),
                (CustomItem)PhantomLantern.Get(typeof(PhantomLantern)),
                (CustomItem)Scp1499.Get(typeof(Scp1499)),
            };
            
            foreach (var item in customItems)
            {
                if (item == null) continue;

                customItemStringBuilder.AppendLine($"Item: {item.Name}");
                customItemStringBuilder.AppendLine($"- Description: {item.Description}");
            }
            
            settings.Add(new SSGroupHeader("VV Custom Items Info"));
            settings.Add(new SSTextArea(null, StringBuilderPool.Shared.ToStringReturn(customItemStringBuilder), SSTextArea.FoldoutMode.CollapsedByDefault));
            
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