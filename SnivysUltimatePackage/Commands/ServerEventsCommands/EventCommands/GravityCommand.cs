using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackage.Commands.ServerEventsCommands.EventCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GravityCommand : ICommand
    {
        public string Command { get; } = "Gravity";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Changes the gravity to be different";
        
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
            response = "Starting Gravity Event";
            Log.Debug($"{sender} has started the Gravity Event");
            return true;
        }
    }
}