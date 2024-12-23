using Exiled.Events.EventArgs.Player;
using GameCore;
using PlayerRoles;
using Log = Exiled.API.Features.Log;

namespace SnivysUltimatePackage.EventHandlers
{
    public class FlamingoAdjustmentEventHandlers
    {
        public Plugin Plugin;
        public FlamingoAdjustmentEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker == null)
                return;
            
            if (ev.Attacker.Role == RoleTypeId.Flamingo || ev.Attacker.Role == RoleTypeId.AlphaFlamingo)
            {
                if (ev.Player.IsScp)
                    ev.Amount = Plugin.Instance.Config.FlamingoAdjustmentsConfig.DamageOnHit * Plugin.Instance.Config.FlamingoAdjustmentsConfig.ScpDamageMultiplier;
                else
                    ev.Amount = Plugin.Instance.Config.FlamingoAdjustmentsConfig.DamageOnHit;
            }
        }
    }
}