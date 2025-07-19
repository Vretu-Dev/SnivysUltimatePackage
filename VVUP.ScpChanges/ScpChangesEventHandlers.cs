using System;
using System.Linq;
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

        public void OnUsingItem(UsedItemEventArgs ev)
        {
            if (Plugin.Instance.ScpChangesEventHandlers == null)
                return;
            if (ev.Item.Type != ItemType.SCP1576)
                return;
            Log.Debug("VVUP SCP 1576 Spectator Viewer: Item is SCP 1576");
            string Scp1576DisplayText = ProcessStringVariables(Plugin.Instance.Config.Scp1576Text);
            Log.Debug($"VVUP SCP 1576 Spectator Viewer: Showing text to {ev.Player.Nickname}");
            ev.Player.ShowHint(Scp1576DisplayText, Plugin.Instance.Config.Scp1576TextDuration);
        }

        public string ProcessStringVariables(string raw)
        {
            Log.Debug("VVUP SCP 1576 Spectator Viewer: Processing String Variables");
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