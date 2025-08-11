using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using UnityEngine;
using UserSettings.ServerSpecific;
using PlayerAPI = Exiled.API.Features.Player;

namespace VVUP.CustomItems
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
                if (ssKeybindSetting.SettingId == Plugin.Instance.Config.DetonateC4Id)
                {
                    if (!Items.Grenades.C4.PlacedCharges.ContainsValue(player))
                    {
                        player.ShowHint(Plugin.Instance.Config.SsssC4NoC4Deployed);
                        player.SendConsoleMessage("\n<color=red>You've haven't placed any C4 charges!</color>", "red");
                        return;
                    }

                    if (Items.Grenades.C4.Instance.RequireDetonator
                        && (player.CurrentItem is null || player.CurrentItem.Type !=
                            Items.Grenades.C4.Instance.DetonatorItem))
                    {
                        player.ShowHint(Plugin.Instance.Config.SsssC4DetonatorNeeded);
                        player.SendConsoleMessage(
                            $"\n<color=red>You need to have a Remote Detonator ({Items.Grenades.C4.Instance.DetonatorItem}) in your hand to detonate C4!</color>",
                            "red");
                        return;
                    }

                    int i = 0;
                    foreach (var charge in Items.Grenades.C4.PlacedCharges.ToList())
                    {
                        if (charge.Value != player)
                            continue;
                        float distance = Vector3.Distance(charge.Key.Position, player.Position);
                        if (distance < Items.Grenades.C4.Instance.MaxDistance)
                        {
                            Items.Grenades.C4.Instance.C4Handler(charge.Key);
                            i++;
                        }
                        else
                        {
                            player.ShowHint(Plugin.Instance.Config.SsssC4TooFarAway);
                            player.SendConsoleMessage(
                                $"One of your charges is out of range. You need to get closer by {Mathf.Round(distance - Items.Grenades.C4.Instance.MaxDistance)} meters.",
                                "yellow");
                        }
                    }

                    player.ShowHint(Plugin.Instance.Config.SsssDetonateC4ActivationMessage);
                    //string response = i == 1 ? $"\n<color=green>{i} C4 charge has been detonated!</color>" : $"\n<color=green>{i} C4 charges have been detonated!</color>"; player.SendConsoleMessage(response, "green");
                }
            }
        }
    }
}