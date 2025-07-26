using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using UserSettings.ServerSpecific;
using VVUP.CustomRoles.Abilities.Active;
using PlayerAPI = Exiled.API.Features.Player;

namespace VVUP.CustomRoles
{
    public class SsssEventHandlers
    {
        public Plugin Plugin;
        public SsssEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnVerified(VerifiedEventArgs ev)
        {
            if (Plugin.Instance.SsssEventHandlers == null)
                return;
            
            Log.Debug($"VVUP: Adding SSSS functions to {ev.Player.Nickname}");
            SsssHelper.SafeAppendSsssSettings();
            ServerSpecificSettingsSync.SendToPlayer(ev.Player.ReferenceHub);
        }

        public static void OnSettingValueReceived(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            if (!PlayerAPI.TryGet(hub, out PlayerAPI player) || hub == null || player == null)
                return;
            if (settingBase is SSKeybindSetting ssKeybindSetting && ssKeybindSetting.SyncIsPressed)
            {
                if ((ssKeybindSetting.SettingId == Plugin.Instance.Config.ActiveCamoId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.ChargeId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.DetectId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.DoorPickingId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.HealingMistId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.RemoveDisguiseId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.ReviveMistId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.TeleportId)
                    && ActiveAbility.AllActiveAbilities.TryGetValue(player, out var abilities))
                {
                    string response = String.Empty;
                    if (ssKeybindSetting.SettingId == Plugin.Instance.Config.ActiveCamoId)
                    {
                        var activeCamoAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ActiveCamo));
                        if (activeCamoAbility != null && activeCamoAbility.CanUseAbility(player, out response))
                        {
                            activeCamoAbility.SelectAbility(player);
                            activeCamoAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssActiveCamoActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.ChargeId)
                    {
                        var chargeAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ChargeAbility));
                        if (chargeAbility != null && chargeAbility.CanUseAbility(player, out response))
                        {
                            chargeAbility.SelectAbility(player);
                            chargeAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssChargeActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.DetectId)
                    {
                        var detectAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(Detect));
                        if (detectAbility != null && detectAbility.CanUseAbility(player, out response))
                        {
                            detectAbility.SelectAbility(player);
                            detectAbility.UseAbility(player);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.DoorPickingId)
                    {
                        var doorPickingAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(DoorPicking));
                        if (doorPickingAbility != null && doorPickingAbility.CanUseAbility(player, out response))
                        {
                            doorPickingAbility.SelectAbility(player);
                            doorPickingAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssDoorPickingActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.HealingMistId)
                    {
                        var healingMistAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(HealingMist));
                        if (healingMistAbility != null && healingMistAbility.CanUseAbility(player, out response))
                        {
                            healingMistAbility.SelectAbility(player);
                            healingMistAbility.UseAbility(player);
                            player.ShowHint(response);
                            player.ShowHint(Plugin.Instance.Config.SsssHealingMistActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.RemoveDisguiseId)
                    {
                        var removeDisguiseAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(RemoveDisguise));
                        if (removeDisguiseAbility != null && removeDisguiseAbility.CanUseAbility(player, out response))
                        {
                            removeDisguiseAbility.SelectAbility(player);
                            removeDisguiseAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssRemoveDisguiseActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.ReviveMistId)
                    {
                        var revivingMistAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(RevivingMist));
                        if (revivingMistAbility != null && revivingMistAbility.CanUseAbility(player, out response))
                        {
                            revivingMistAbility.SelectAbility(player);
                            revivingMistAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssReviveMistActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.TeleportId)
                    {
                        var teleportAbility =
                            abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(Teleport));
                        if (teleportAbility != null && teleportAbility.CanUseAbility(player, out response))
                        {
                            teleportAbility.SelectAbility(player);
                            teleportAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssTeleportActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                }
            }
        }
    }
}