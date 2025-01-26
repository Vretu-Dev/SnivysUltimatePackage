using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SnivysUltimatePackage.EventHandlers
{
    public class MicroDamageReductionEventHandler
    {
        public Plugin Plugin;
        public MicroDamageReductionEventHandler(Plugin plugin) => Plugin = plugin;

        public void OnPlayerHurting(HurtingEventArgs ev)
        {
            Log.Debug("VVUP Micro Damage Reduction: Checking if Micro Damage Reduction is enabled");
            if (!Plugin.Instance.Config.MicroDamageReductionConfig.IsEnabled)
                return;
            if (ev.Player == null)
                return;
            if (Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReduction == null)
                return;
            
            Log.Debug("VVUP Micro Damage Reduction: Checking if damage can be reduced");
            if (ev.Attacker != ev.Player && ev.DamageHandler.Type == DamageType.MicroHid &&
                Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReduction.Contains(ev.Player.Role))
            {
                if (Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReductionValue == 0)
                {
                    Log.Debug("VVUP Micro Damage Reduction: ScpDamageReductionValue is 0, you cannot divide by 0, doing normal damage");
                    return;
                }
                ev.Amount /= Plugin.Instance.Config.MicroDamageReductionConfig.ScpDamageReductionValue;
                Log.Debug($"VVUP Micro Damage Reduction: {ev.Player.Nickname} is {ev.Player.Role}, reducing damage");
            }
        }
    }
}
