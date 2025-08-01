using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerAPI = Exiled.API.Features.Player;
using PlayerEvent = Exiled.Events.Handlers.Player;

namespace VVUP.ServerEvents.ServerEventsEventHandlers
{
    public class ItemRandomizerEventHandlers
    {
        private static bool _irStarted;
        public ItemRandomizerEventHandlers()
        {
            if (_irStarted)
            {
                Log.Debug("VVUP Server Events, Item Randomizer: Event already started.");
                return;
            }
            Log.Debug("VVUP Server Events, Item Randomizer: Starting Item Randomizer Event.");
            _irStarted = true;
            Plugin.ActiveEvent += 1;
            PlayerEvent.PickingUpItem += Plugin.Instance.ServerEventsMainEventHandler.OnPickingUpItemIR;
            PlayerEvent.DroppingItem += Plugin.Instance.ServerEventsMainEventHandler.OnDroppingItemIR;
        }
        public static void EndEvent()
        {
            if (!_irStarted)
            {
                Log.Debug("VVUP Server Events, Item Randomizer: Event not started.");
                return;
            }
            Log.Debug("VVUP Server Events, Item Randomizer: Ending Item Randomizer Event.");
            _irStarted = false;
            Plugin.ActiveEvent -= 1;
            PlayerEvent.PickingUpItem -= Plugin.Instance.ServerEventsMainEventHandler.OnPickingUpItemIR;
            PlayerEvent.DroppingItem -= Plugin.Instance.ServerEventsMainEventHandler.OnDroppingItemIR;
        }
    }
}