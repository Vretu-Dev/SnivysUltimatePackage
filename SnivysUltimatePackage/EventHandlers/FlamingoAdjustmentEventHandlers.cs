using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using PluginAPI.Enums;

namespace SnivysUltimatePackage.EventHandlers
{
    public class FlamingoAdjustmentEventHandlers
    {
        public Plugin Plugin;
        public FlamingoAdjustmentEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnHurting(HurtingEventArgs ev)
        {
            if (!Plugin.Instance.Config.FlamingoAdjustmentsConfig.IsEnabled)
                return;
            if (ev.Attacker != ev.Player && ev.Attacker != null && ev.Player != null && 
                (ev.Attacker.Role == RoleTypeId.AlphaFlamingo || ev.Attacker.Role == RoleTypeId.Flamingo))
            {
                float damageAmount = Plugin.Instance.Config.FlamingoAdjustmentsConfig.DamageOnHit;
                
                if (ev.Player.IsScp)
                    ev.Amount = damageAmount * Plugin.Instance.Config.FlamingoAdjustmentsConfig.ScpDamageMultiplier;
                else
                    ev.Amount = damageAmount;
            }
        }
    }
}