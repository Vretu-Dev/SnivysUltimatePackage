using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackageOneConfig.Commands.ServerEventsCommands.EventCommands
{
    internal class OperationCrossfireCommand : ICommand
    {
        public string Command { get; set; } = "OperationCrossfire";
        public string[] Aliases { get; set; } = Array.Empty<string>();
        public string Description { get; set; } = "Operation Crossfire is a more RP, Military Sim style event, where teams will have goals to achieve.";
        private static ServerEventsMasterConfig _config = new();
        public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
        {
            if (!_config.IsEnabled)
            {
                response = "The custom events part of this plugin is disabled.";
                return false;
            }
            
            if (!sender.CheckPermission("vvevents.rund"))
            {
                response = "You do not have the required permission to use this command";
                return false;
            }
            
            if (Plugin.ActiveEvent != 0)
            {
                response = "An event is already running, this event will conflict heavily with everything else. Not running. Some events might be able to run if you start them after this one";
                return false;
            }
            
            OperationCrossfireEventHandlers operationCrossfire = new OperationCrossfireEventHandlers();
            response = "Starting Operation Crossfire Event";
            Log.Debug($"{sender} has started Operation Crossfire Event");
            return true;
        }
    }
}