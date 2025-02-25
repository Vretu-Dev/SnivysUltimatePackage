using Exiled.API.Features;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using UnityEngine;
using PlayerLab = LabApi.Features.Wrappers.Player;

namespace SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers
{
    public class GravityEventHandlers
    {
        private static GravityConfig _config;
        private static bool _gravStarted;

        public GravityEventHandlers()
        {
            Log.Debug("VVUP Server Events, Gravity: Checking if the gravity event has already started");
            if (_gravStarted)
                return;
            _config = Plugin.Instance.Config.ServerEventsMasterConfig.GravityConfig;
            Log.Debug("VVUP Server Events, Gravity: Registering OnChangingRoles (GE) event handler");
            Exiled.Events.Handlers.Player.ChangingRole += Plugin.Instance.ServerEventsMainEventHandler.OnRoleSwapGE;
            Plugin.ActiveEvent += 1;
            Cassie.MessageTranslated(_config.StartEventCassieMessage, _config.StartEventCassieText);
            foreach (var player in Player.List)
            {
                Log.Debug($"VVUP Server Events, Gravity: Setting {player.Nickname}'s gravity to {GetPlayerGravity()}");
                PlayerLab.Get(player.ReferenceHub)!.Gravity = GetPlayerGravity();
            }
        }
        public static Vector3 GetPlayerGravity()
        {
            Log.Debug("VVUP Server Events, Gravity: Getting Gravity");
            return _config.GravityChanges;
        }
        public static void EndEvent()
        {
            if (!_gravStarted) return;
            Log.Debug("VVUP Server Events, Gravity: Unregistering ChangingRole (GE) Event Handlers");
            Exiled.Events.Handlers.Player.ChangingRole -= Plugin.Instance.ServerEventsMainEventHandler.OnRoleSwapGE;
            _gravStarted = false;
            Plugin.ActiveEvent -= 1;
            foreach (var player in Player.List)
            {
                Log.Debug($"VVUP Server Events, Gravity: Setting {player.Nickname}'s gravity back to normal");
                PlayerLab.Get(player.ReferenceHub)!.Gravity = new Vector3(1, 1, 1);
            }
        }
    }
}