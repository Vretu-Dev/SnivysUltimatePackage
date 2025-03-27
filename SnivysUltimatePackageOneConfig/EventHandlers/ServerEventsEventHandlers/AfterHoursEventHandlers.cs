using Exiled.API.Features;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using UnityEngine;
using PlayerEvent = Exiled.Events.Handlers.Player;

namespace SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers
{
    public class AfterHoursEventHandlers
    {
        private static AfterHoursConfig _config;
        private static bool _ahStarted;

        public AfterHoursEventHandlers()
        {
            Log.Debug("VVUP Server Events, After Hours: Checking if After Hours Event has already started");
            if (_ahStarted) return;
            _config = Plugin.Instance.Config.ServerEventsMasterConfig.AfterHoursConfig;
            Plugin.ActiveEvent += 1;
            _ahStarted = true;
            Log.Debug("VVUP Server Events, After Hours: Dimming the lights, reducing tesla activation chance, reducing intercom time");
            foreach (Room room in Room.List)
            {
                room.Color = new Color(0.25f, 0.25f, 0.25f);
            }
            PlayerEvent.TriggeringTesla += Plugin.Instance.ServerEventsMainEventHandler.OnTeslaActivationAh;
            Intercom.SpeechRemainingTime = _config.IntercomTime;
            Cassie.MessageTranslated(_config.StartEventCassieMessage, _config.StartEventCassieText);
        }

        public static void EndEvent()
        {
            if (!_ahStarted) return;
            PlayerEvent.TriggeringTesla -= Plugin.Instance.ServerEventsMainEventHandler.OnTeslaActivationAh;
            _ahStarted = false;
            Plugin.ActiveEvent -= 1;
        }
    }
}