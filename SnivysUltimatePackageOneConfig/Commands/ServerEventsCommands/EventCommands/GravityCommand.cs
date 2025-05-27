using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackageOneConfig.Commands.ServerEventsCommands.EventCommands
{
    internal class GravityCommand : ICommand
    {
        public string Command { get; set; } = "LowGravity";
        public string[] Aliases { get; set; } = { "Moon", "LG" };
        public string Description { get; set; } = "Starts the Low Gravity Event";
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
            
            GravityEventHandlers gravityEventHandlers = new GravityEventHandlers();
            response = "Starting Low Gravity Event";
            Log.Debug($"{sender} has started the Low Gravity Event");
            return true;
        }
    }
}