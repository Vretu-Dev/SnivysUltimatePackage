using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using MEC;
namespace SnivysUltimatePackage.EventHandlers;

public class EscapeDoorOpenerEventHandlers
{
    public Plugin Plugin;
    public EscapeDoorOpenerEventHandlers(Plugin plugin) => Plugin = plugin;

    public void OnRoundStarted()
    {
        Timing.CallDelayed(1.5f, () =>
        {
            var escapeDoor = DoorType.EscapeFinal;
            Door door = Door.Get(escapeDoor);
            door.IsOpen = true;
        });
    }
}