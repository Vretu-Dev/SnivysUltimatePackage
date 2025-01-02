using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackage.Configs.ServerEventsConfigs;
using SnivysUltimatePackage.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackage.Commands.ServerEventsCommands.EventCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class ShortCommand : ICommand
    {
        public string Command { get; set; } = "ShortPeople";
        public string[] Aliases { get; set; } = { "Dwarf", "Tiny" };
        public string Description { get; set; } = "Starts the Short People Event";
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
            var shortEventHandlers = new ShortEventHandlers();
            response = "Starting Short People Event";
            Log.Debug($"{sender} has started the Short People Event");
            return true;
        }
    }
}