using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;

namespace SnivysUltimatePackageOneConfig.EventHandlers
{
    public class EscapeDoorOpenerEventHandlers
    {
        public Plugin Plugin;
        public EscapeDoorOpenerEventHandlers(Plugin plugin) => Plugin = plugin;

        public void OnRoundStarted()
        {
            Log.Debug("VVUP Escape Door Opener: Checking if Escape Door Opener is enabled");
            if (!Plugin.Instance.Config.EscapeDoorOpenerConfig.IsEnabled)
                return;
            Timing.CallDelayed(1.5f, () =>
            {
                Log.Debug("VVUP Escape Door Opener: Opening Escape Final Door on Surface");
                var escapeDoor = DoorType.EscapeFinal;
                Door door = Door.Get(escapeDoor);
                door.IsOpen = true;
            });
        }
    }
}