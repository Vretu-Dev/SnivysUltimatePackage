using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SnivysUltimatePackageOneConfig.Configs.ServerEventsConfigs;
using SnivysUltimatePackageOneConfig.EventHandlers.ServerEventsEventHandlers;

namespace SnivysUltimatePackageOneConfig.Commands.ServerEventsCommands.EventCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class NameRedactedCommand : ICommand
    {
        public string Command { get; set; } = "NameRedacted";
        public string[] Aliases { get; set; } = Array.Empty<string>();
        public string Description { get; set; } = "Removes player's nicknames and sets them to something else";
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
            NameRedactedEventHandlers nameRedactedHandler = new NameRedactedEventHandlers();
            response = "Starting Name Redacted Event";
            Log.Debug($"{sender} has started the Name Redacted Event");
            return true;
        }
    }
}