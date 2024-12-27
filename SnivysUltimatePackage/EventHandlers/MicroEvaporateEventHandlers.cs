using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace SnivysUltimatePackage.EventHandlers
{
    public class MicroEvaporateEventHandlers
    {
        public Plugin Plugin;
        public MicroEvaporateEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnDying(DyingEventArgs ev)
        {
            if (!Plugin.Instance.Config.MicroEvaporateConfig.IsEnabled)
                return;
            if (ev.DamageHandler.Type == DamageType.MicroHid)
                ev.Player.Vaporize();
        }
    }
}