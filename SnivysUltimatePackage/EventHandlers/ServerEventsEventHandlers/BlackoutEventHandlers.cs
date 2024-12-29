using Exiled.API.Features;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using Maps = Exiled.Events.Handlers.Map;

namespace SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers
{
    public class BlackoutEventHandlers
    {
        private static BlackoutConfig _config;
        private static bool _boeStarted;
        public BlackoutEventHandlers()
        {
            Log.Debug("VVUP Server Events, Blackout: Checking if Blackout Event has already started");
            if (_boeStarted) return;
            _config = Plugin.Instance.Config.ServerEventsMasterConfig.BlackoutConfig;
            Plugin.ActiveEvent += 1;
            if (_config.GeneratorEndsEvent)
            {
                Log.Debug("VVUP Server Events, Blackout: Enabling the Generator Activation Event Handlers");
                Maps.GeneratorActivating += Plugin.Instance.ServerEventsMainEventHandler.OnGeneratorEngagedBOE;
            }
            
            _boeStarted = true;
            Log.Debug("VVUP Server Events, Blackout: Turning off the lights");
            Map.TurnOffAllLights(432000);
            foreach (var player in Player.List)
            {
                Log.Debug($"VVUP Server Events, Blackout: Adding {_config.BlackoutEventStartingItem} to {player}");
                player.AddItem(_config.BlackoutEventStartingItem);
            }

            Cassie.MessageTranslated(_config.StartEventCassieMessage, _config.StartEventCassieText);
        }

        public static void EndEvent()
        {
            if (!_boeStarted) return;
            Cassie.MessageTranslated(_config.EndEventCassieMessage, _config.EndEventCassieText);
            if (_config.GeneratorEndsEvent)
            {
                Log.Debug("VVUP Server Events, Blackout: Disabling Generator Engaged Event Handler");
                Maps.GeneratorActivating -= Plugin.Instance.ServerEventsMainEventHandler.OnGeneratorEngagedBOE;
            }
            Log.Debug("VVUP Server Events, Blackout: Turning on the lights");
            Map.TurnOffAllLights(1);
            _boeStarted = false;
            Plugin.ActiveEvent -= 1;
        }
    }
}