using Exiled.API.Features;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using Player = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers
{
    
    public class PeanutHydraEventHandlers
    {
        public static PeanutHydraConfig Config;
        private static bool _pheStarted;
        public PeanutHydraEventHandlers()
        {
            Log.Debug("Checking if Peanut Hydra has already been started");
            if (_pheStarted) return;
            Config = Plugin.Instance.Config.ServerEventsMasterConfig.PeanutHydraConfig;
            Plugin.ActiveEvent += 1;
            Log.Debug("Adding On Dying and On Died Event PHE Handlers");
            Player.Dying += Plugin.Instance.ServerEventsMainEventHandler.OnDyingPHE;
            Player.Died += Plugin.Instance.ServerEventsMainEventHandler.OnDiedPHE;
            _pheStarted = true;
            Cassie.MessageTranslated(Config.StartEventCassieMessage, Config.StartEventCassieText);
        }
        public static void EndEvent()
        {
            if (!_pheStarted) return;
            Log.Debug("Removing On Dying and On Died Event PHE Handlers");
            Player.Dying += Plugin.Instance.ServerEventsMainEventHandler.OnDyingPHE;
            Player.Died -= Plugin.Instance.ServerEventsMainEventHandler.OnDiedPHE;
            _pheStarted = false;
            Plugin.ActiveEvent -= 1;
        }
    }
}