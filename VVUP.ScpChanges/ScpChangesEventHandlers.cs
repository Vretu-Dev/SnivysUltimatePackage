using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Respawning;
using Respawning.Waves;

namespace VVUP.ScpChanges
{
    public class ScpChangesEventHandlers
    {
        public Plugin Plugin;
        public ScpChangesEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (Plugin.Instance.ScpChangesEventHandlers == null)
                return;
            if (ev.Player == null)
                return;
            if (ev.NewRole == RoleTypeId.Scp106 && Plugin.Instance.Config.OldScp106Behavior)
            {
                Log.Debug("VVUP SCP Changes: Old SCP 106 Behavior is enabled, setting health and damage resistance");
                ev.Player.MaxHealth = 600;
                ev.Player.Health = 600;
            }
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            if (Plugin.Instance.ScpChangesEventHandlers == null)
                return;
            if (ev.Player == null || ev.Attacker == null)
                return;
            if (ev.Player.Role == RoleTypeId.Scp106 && Plugin.Instance.Config.OldScp106Behavior && ev.DamageHandler.Type == DamageType.Firearm)
            {
                Log.Debug("VVUP SCP Changes: Old SCP 106 Behavior is enabled, setting damage resistance");
                ev.Amount = (float)(ev.Amount * 0.1);
                Log.Debug($"VVUP SCP Changes: Reduced damage to {ev.Amount}");
            }
        }
        public void OnUsingItem(UsedItemEventArgs ev)
        {
            if (Plugin.Instance.ScpChangesEventHandlers == null)
                return;
            if (ev.Item.Type != ItemType.SCP1576)
                return;
            Log.Debug("VVUP SCP Changes: Item is SCP 1576");
            string Scp1576DisplayText = ProcessStringVariables(Plugin.Instance.Config.Scp1576Text);
            Log.Debug($"VVUP SCP Changes: Showing text to {ev.Player.Nickname}");
            ev.Player.ShowHint(Scp1576DisplayText, Plugin.Instance.Config.Scp1576TextDuration);
        }

        public string ProcessStringVariables(string raw)
        {
            Log.Debug("VVUP SCP Changes: Processing String Variables");
            var replace = raw.Replace("%spectators%",
                Player.List.Count(p => p.Role.Type == RoleTypeId.Spectator).ToString());
            float timeBeforeSpawn = 0;
            foreach (TimeBasedWave wave in WaveManager.Waves)
            {
                timeBeforeSpawn = wave.Timer.TimeLeft;
            }
            var actualText = replace.Replace("%timebeforespawnwave%", Math.Floor(timeBeforeSpawn).ToString());
            return actualText;
        }
    }
}