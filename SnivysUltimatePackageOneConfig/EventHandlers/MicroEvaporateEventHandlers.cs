using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace SnivysUltimatePackageOneConfig.EventHandlers
{
    public class MicroEvaporateEventHandlers(Plugin plugin)
    {
        public Plugin Plugin = plugin;

        public void OnDying(DyingEventArgs ev)
        {
            if (Plugin.Instance.MicroEvaporateEventHandlers == null)
                return;
            Log.Debug("VVUP Micro Evaporate: Checking if Micro Evaporate is enabled");
            if (!Plugin.Instance.Config.MicroEvaporateConfig.IsEnabled)
                return;
            Log.Debug("VVUP Micro Evaporate: Micro Evaporate is enabled, checking damage type");
            if (ev.DamageHandler.Type == DamageType.MicroHid)
            {
                Log.Debug($"VVUP Micro Evaporate: Damage type is MicroHID, vaporizing {ev.Player.Nickname}");
                ev.Player.Vaporize();
            }
        }
    }
}