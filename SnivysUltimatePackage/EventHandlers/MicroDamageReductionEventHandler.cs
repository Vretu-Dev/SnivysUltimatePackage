using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerStatsSystem;

namespace SnivysUltimatePackage.EventHandlers
{
    public class MicroDamageReductionEventHandler
    {
        public Plugin Plugin;
        public MicroDamageReductionEventHandler(Plugin plugin) => Plugin = plugin;

        public void OnPlayerHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != ev.Player && ev.DamageHandler.Base is MicroHidDamageHandler &&
                Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReduction.Contains(ev.Player.Role))
            {
                ev.Amount /= Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReductionValue;
                Log.Debug("SCP is on the list, damage is Micro");
            }
        }
    }
}