using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackage.Commands.ServerEventsCommands.EventCommands
{
    internal class VariableLightCommand : ICommand
    {
        public string Command { get; set; } = "VariableLights";
        public string[] Aliases { get; set; } = { "RandomLights", "ColorfulLights" };
        public string Description { get; set; } = "Starts the Variable Lights Event. (PHOTOSENSITIVITY WARNING!)";
        private static ServerEventsMasterConfig _config = new();
        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            if (!_config.IsEnabled)
            {
                response = "The custom events part of this plugin is disabled.";
                return false;
            }
            
            if (!sender.CheckPermission("vvevents.run"))
            {
                response = "You do not have the required permission to use this command";
                return false;
            }
            VariableLightsEventHandlers variableEventHandlers = new VariableLightsEventHandlers();
            response = "Starting Variable Lights Event.";
            Log.Debug($"{sender} has started the Variable Lights Event");
            return true;
        }
    }
}