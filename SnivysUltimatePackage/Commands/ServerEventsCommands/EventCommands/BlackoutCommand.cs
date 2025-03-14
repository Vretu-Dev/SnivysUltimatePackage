using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackage.Commands.ServerEventsCommands.EventCommands
{
    internal class BlackoutCommand : ICommand
    {
        public string Command { get; set; } = "Blackout";
        public string[] Aliases { get; set; } = { "LightsOut" };
        public string Description { get; set; } = "Starts the Blackout Event";
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
            
            BlackoutEventHandlers blackoutEventHandlers = new BlackoutEventHandlers();
            response = "Starting Blackout Event";
            Log.Debug($"{sender} has started the Blackout Event");
            return true;
        }
    }
}