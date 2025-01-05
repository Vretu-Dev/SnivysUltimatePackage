using System.Linq;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PluginAPI.Core;
using SnivysCustomRolesAbilities.Abilities;
using SnivysUltimatePackage.Custom.Abilities;
using UserSettings.ServerSpecific;
using Player = Exiled.API.Features.Player;

namespace SnivysUltimatePackage.EventHandlers
{
    public class SsssEventHandler
    {
        public Plugin Plugin;
        public SsssEventHandler(Plugin plugin) => Plugin = plugin;

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Log.Debug("VVUP: Adding SSSS functions to players");
            ServerSpecificSettingsSync.DefinedSettings = Ssss.VVUltimatePluginPackage();
            ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);
        }

        public void OnSettingValueReceived(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            if (!Player.TryGet(hub, out Player player))
                return;

            if (settingBase is SSKeybindSetting ssKeybindSetting /*&& ssKeybindSetting.SettingId == 10000*/ &&
                ssKeybindSetting.SyncIsPressed && ActiveAbility.AllActiveAbilities.TryGetValue(player, out var abilities))
            {
                switch (ssKeybindSetting.SettingId)
                {
                    case 10000:
                    {
                        var activeCamoAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ActiveCamo));
                        if (activeCamoAbility != null && activeCamoAbility.CanUseAbility(player, out string response))
                        {
                            activeCamoAbility.SelectAbility(player);
                            activeCamoAbility.UseAbility(player);
                        }

                        break;
                    }
                    case 10001:
                    {
                        var chargeAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ChargeAbility));
                        if (chargeAbility != null && chargeAbility.CanUseAbility(player, out string response))
                        {
                            chargeAbility.SelectAbility(player);
                            chargeAbility.UseAbility(player);
                        }

                        break;
                    }
                    case 10002:
                    {
                        var detectAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(Detect));
                        if (detectAbility != null && detectAbility.CanUseAbility(player, out string response))
                        {
                            detectAbility.SelectAbility(player);
                            detectAbility.UseAbility(player);
                        }

                        break;
                    }
                    case 10003:
                    {
                        var doorPickingAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(DoorPicking));
                        if (doorPickingAbility != null && doorPickingAbility.CanUseAbility(player, out string response))
                        {
                            doorPickingAbility.SelectAbility(player);
                            doorPickingAbility.UseAbility(player);
                        }

                        break;
                    }
                    case 10004:
                    {
                        var healingMistAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(HealingMist));
                        if (healingMistAbility != null && healingMistAbility.CanUseAbility(player, out string response))
                        {
                            healingMistAbility.SelectAbility(player);
                            healingMistAbility.UseAbility(player);
                        }

                        break;
                    }
                    case 10005:
                    {
                        var removeDisguiseAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(RemoveDisguise));
                        if (removeDisguiseAbility != null && removeDisguiseAbility.CanUseAbility(player, out string response))
                        {
                            removeDisguiseAbility.SelectAbility(player);
                            removeDisguiseAbility.UseAbility(player);
                        }

                        break;
                    }
                }
            }
        }
    }
}