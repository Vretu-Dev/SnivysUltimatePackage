using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomRoles.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using SnivysUltimatePackage.Custom.Abilities;
using UnityEngine;
using UserSettings.ServerSpecific;
using PlayerAPI = Exiled.API.Features.Player;

namespace SnivysUltimatePackage.EventHandlers
{
    public class SsssEventHandler
    {
        public Plugin Plugin;
        public SsssEventHandler(Plugin plugin) => Plugin = plugin;

        public void OnRoundStarted()
        {
            if (!Plugin.Instance.Config.SsssConfig.IsEnabled)
                return;
            
            foreach (PlayerAPI player in Player.List)
            {
                Log.Debug($"VVUP: Adding SSSS functions to {player.Nickname}");
                try
                {
                    ServerSpecificSettingsSync.DefinedSettings = SsssHelper.GetSettings();
                    ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub);
                }
                catch (InvalidCastException ex)
                {
                    Log.Error($"VVUP: InvalidCastException occurred: {ex.Message}");
                }
            }
        }

        public void OnSettingValueReceived(ReferenceHub hub, ServerSpecificSettingBase settingBase)
        {
            if (!Plugin.Instance.Config.SsssConfig.IsEnabled)
                return;
            
            if (!Player.TryGet(hub, out Player player))
                return;

            if (player == null)
                return;

            if (settingBase is SSKeybindSetting ssKeybindSetting && ssKeybindSetting.SyncIsPressed)
            {
                if ((ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.ActiveCamoId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.ChargeId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.DetectId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.DoorPickingId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.HealingMistId
                     || ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.RemoveDisguiseId)
                    && ActiveAbility.AllActiveAbilities.TryGetValue(player, out var abilities))
                {
                    string response = String.Empty;
                    if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.ActiveCamoId)
                    {
                        var activeCamoAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ActiveCamo));
                        if (activeCamoAbility != null && activeCamoAbility.CanUseAbility(player, out response))
                        {
                            activeCamoAbility.SelectAbility(player);
                            activeCamoAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssActiveCamoActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.ChargeId)
                    {
                        var chargeAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(ChargeAbility));
                        if (chargeAbility != null && chargeAbility.CanUseAbility(player, out response))
                        {
                            chargeAbility.SelectAbility(player);
                            chargeAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssChargeActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.DetectId)
                    {
                        var detectAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(Detect));
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
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.DoorPickingId)
                    {
                        var doorPickingAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(DoorPicking));
                        if (doorPickingAbility != null && doorPickingAbility.CanUseAbility(player, out response))
                        {
                            doorPickingAbility.SelectAbility(player);
                            doorPickingAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssDoorPickingActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.HealingMistId)
                    {
                        var healingMistAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(HealingMist));
                        if (healingMistAbility != null && healingMistAbility.CanUseAbility(player, out response))
                        {
                            healingMistAbility.SelectAbility(player);
                            healingMistAbility.UseAbility(player);player.ShowHint(response);
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssHealingMistActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                    else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.RemoveDisguiseId)
                    {
                        var removeDisguiseAbility = abilities.FirstOrDefault(abilities => abilities.GetType() == typeof(RemoveDisguise));
                        if (removeDisguiseAbility != null && removeDisguiseAbility.CanUseAbility(player, out response))
                        {
                            removeDisguiseAbility.SelectAbility(player);
                            removeDisguiseAbility.UseAbility(player);
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssRemoveDisguiseActivationMessage);
                        }
                        else
                        {
                            player.ShowHint(response);
                        }
                    }
                }
                else if (ssKeybindSetting.SettingId == Plugin.Instance.Config.SsssConfig.DetonateC4Id)
                {
                    if (!SnivysUltimatePackage.Custom.Items.Grenades.C4.PlacedCharges.ContainsValue(player))
                    {
                        player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssC4NoC4Deployed);
                        player.SendConsoleMessage("\n<color=red>You've haven't placed any C4 charges!</color>", "red"); 
                        return;
                    }

                    if (SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.RequireDetonator 
                        && (player.CurrentItem is null || player.CurrentItem.Type !=
                            SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.DetonatorItem))
                    {
                        player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssC4DetonatorNeeded);
                        player.SendConsoleMessage($"\n<color=red>You need to have a Remote Detonator ({SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.DetonatorItem}) in your hand to detonate C4!</color>", "red"); 
                        return;
                    } 
                    int i = 0;
                    foreach (var charge in SnivysUltimatePackage.Custom.Items.Grenades.C4.PlacedCharges.ToList())
                    {
                        if (charge.Value != player) 
                            continue; 
                        float distance = Vector3.Distance(charge.Key.Position, player.Position);
                        if (distance < SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.MaxDistance)
                        {
                            SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.C4Handler(charge.Key); i++;
                        }
                        else
                        {
                            player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssC4TooFarAway);
                            player.SendConsoleMessage($"One of your charges is out of range. You need to get closer by {Mathf.Round(distance - SnivysUltimatePackage.Custom.Items.Grenades.C4.Instance.MaxDistance)} meters.", "yellow");
                        }
                    } 
                    player.ShowHint(Plugin.Instance.Config.SsssConfig.SsssDetonateC4ActivationMessage);
                    //string response = i == 1 ? $"\n<color=green>{i} C4 charge has been detonated!</color>" : $"\n<color=green>{i} C4 charges have been detonated!</color>"; player.SendConsoleMessage(response, "green");
                }
            }
        }
    }
}