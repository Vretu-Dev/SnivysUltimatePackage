using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackage.Commands.ServerEventsCommands.EventCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class SnowballsVsScpsCommand : ICommand
    {
        public string Command { get; set; } = "SnowballsVsScps";
        public string[] Aliases { get; set; } = { "Svs", "SnowballFight" };
        public string Description { get; set; } = "Starts the Snowballs Vs Scps Event";
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
            
            var blackoutEventHandlers = new BlackoutEventHandlers();
            response = "Starting Blackout Event";
            Log.Debug($"{sender} has started the Blackout Event");
            return true;
        }
    }
}