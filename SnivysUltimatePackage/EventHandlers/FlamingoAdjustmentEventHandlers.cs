using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace SnivysUltimatePackage.EventHandlers
{
    public class FlamingoAdjustmentEventHandlers
    {
        public Plugin Plugin;
        public FlamingoAdjustmentEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnHurting(HurtingEventArgs ev)
        {
            Log.Debug("VVUP Flamingo Adjustment: Checking if Flamingo Adjustments are enabled");
            if (!Plugin.Instance.Config.FlamingoAdjustmentsConfig.IsEnabled)
                return;
            Log.Debug("VVUP Flamingo Adjustment: Flamingo Adjustments are enabled, checking if attackers are flamingos");
            if (ev.Attacker != ev.Player && ev.Attacker != null && ev.Player != null && 
                (ev.Attacker.Role == RoleTypeId.AlphaFlamingo || ev.Attacker.Role == RoleTypeId.Flamingo))
            {
                Log.Debug("VVUP Flamingo Adjustment: Attacker is a Flamingo, adjusting damage");
                float damageAmount = Plugin.Instance.Config.FlamingoAdjustmentsConfig.DamageOnHit;
                
                if (ev.Player.IsScp)
                    ev.Amount = damageAmount * Plugin.Instance.Config.FlamingoAdjustmentsConfig.ScpDamageMultiplier;
                else
                    ev.Amount = damageAmount;
            }
        }
    }
}