using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;
using SnivysUltimatePackage.Configs;

namespace SnivysUltimatePackage.Commands.VotingCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class StartVote : ICommand
    {
        public string Command { get; } = "StartVote";
        public string[] Aliases { get; } = ["SV"];
        public string Description { get; } = "Starts a vote, shown to all players";
        public static bool IsVoteActive { get; set; } = false;
        public static Dictionary<int, string> VoteOptions = new Dictionary<int, string>();
        public static Dictionary<string, int> PlayerVotes = new Dictionary<string, int>();
        //public static 
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
        {
            if (!VoteConfig.IsEnabled)
            {
                response = "This command is disabled.";
                return false;
            }
            
            if (!sender.CheckPermission("vvvotes.start"))
            {
                response = "You do not have permission to use this command.";
                return false;
            }

            if (IsVoteActive)
            {
                response = "A vote is already active, please wait until it finishes";
                return false;
            }

            if (arguments.Count is < 3 or > 7)
            {
                response =
                    "You provided an invalid amount of arguments \n Order: <Vote Name> <Option 1> <Option 2> [Option 3-7]";
                return false;
            }
            
            VoteOptions.Clear();
            PlayerVotes.Clear();
            
            for (int i = 1; i <= arguments.Count; i++)
                VoteOptions[i] = arguments.At(i);
            
            IsVoteActive = true;
            string optionsMessage = string.Join(",", VoteOptions.Select(pair => $"{pair.Key}: {pair.Value}"));
            Map.Broadcast(VoteConfig.MapBroadcastTime, $"{VoteConfig.MapBroadcastText} Options: {optionsMessage}", Broadcast.BroadcastFlags.Normal, true);

            Timing.CallDelayed(VoteConfig.VoteDuration, () =>
            {
                var results = PlayerVotes.GroupBy(x => x.Value)
                    .Select(group => new { Option = group.Key, Count = group.Count() })
                    .OrderByDescending(x => x.Count);
                string resultMessage = "<size=30>The vote has ended.</size>";
                foreach (var result in results)
                {
                    resultMessage += $"<size=30>{VoteOptions[result.Option]} : {result.Count} votes.</size>";
                }
                Map.Broadcast(VoteConfig.MapBroadcastTime, resultMessage, Broadcast.BroadcastFlags.Normal, true);
            });
            response = "You have started a vote";
            return true;
        }
    }
}