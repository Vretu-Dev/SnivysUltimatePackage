using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features.Items;
using Exiled.API.Features.Waves;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using InventorySystem.Items.Usables.Scp1576;
using PlayerRoles;
using Respawning;
using Respawning.Waves;
using WaveTimer = Respawning.Waves.WaveTimer;

namespace SnivysUltimatePackage.EventHandlers
{
    public class Scp1576SpectatorViewerEventHandlers
    {
        public Plugin Plugin;
        public Scp1576SpectatorViewerEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnUsingItem(UsedItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.SCP1576)
                return;
            //if (Scp1576Item._eventAssigned)
            //    return;
            string Scp1576DisplayText = ProcessStringVariables(Plugin.Instance.Config.Scp1576SpectatorViewerConfig.Scp1576Text);
            ev.Player.ShowHint(Scp1576DisplayText, Plugin.Instance.Config.Scp1576SpectatorViewerConfig.Scp1576TextDuration);
        }

        public string ProcessStringVariables(string raw)
        {
            var replace = raw.Replace("%spectators%",
                Exiled.API.Features.Player.List.Count(p => p.Role.Type == RoleTypeId.Spectator).ToString());
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