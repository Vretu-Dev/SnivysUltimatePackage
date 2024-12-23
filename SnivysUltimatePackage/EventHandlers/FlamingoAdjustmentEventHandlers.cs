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
            if (ev.Attacker == null)
                return;
            
            if (ev.Attacker.Role == RoleTypeId.Flamingo || ev.Attacker.Role == RoleTypeId.AlphaFlamingo)
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