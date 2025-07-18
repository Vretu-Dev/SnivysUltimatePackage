using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.CustomItems.API;
using Exiled.CustomItems.API.Features;
using Exiled.CustomRoles.API;
using Exiled.CustomRoles.API.Features;
using Exiled.Loader;
using VVUP.CustomRoles.API;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace VVUP.HuskInfection
{
    public class Plugin : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.Lower;
        public static Plugin Instance;
        public override string Name { get; } = "Snivy's Husk Infection";
        public override string Author { get; } = "Vicious Vikki";
        public override string Prefix { get; } = "VVUP.HK";
        public override Version Version { get; } = Base.Plugin.Instance.Version;
        public override Version RequiredExiledVersion { get; } = Base.Plugin.Instance.RequiredExiledVersion;

        public HuskInfectionEventHandlers HuskInfectionEventHandlers;
        
        public override void OnEnabled()
        {
            Instance = this;
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.Base"))
            {
                Log.Error("VVUP: Base Plugin is not present, disabling module");
                base.OnDisabled();
                return;
            }

            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.CustomRoles"))
            {
                Log.Error("VVUP: Custom Roles Module is not present, disabling module");
                base.OnDisabled();
                return;
            }
            if (!Loader.Plugins.Any(plugin => plugin.Prefix == "VVUP.CustomItems"))
            {
                Log.Error("VVUP: Custom Items Module is not present, disabling module");
                base.OnDisabled();
                return;
            }
            Config.HuskZombies.Register();
            foreach (CustomRole role in CustomRole.Registered)
            {
                if (role is ICustomRole custom)
                {
                    Log.Debug($"Adding {role.Name} to dictionary..");
                    StartTeam team;
                    if (custom.StartTeam.HasFlag(StartTeam.Chaos))
                        team = StartTeam.Chaos;
                    else if (custom.StartTeam.HasFlag(StartTeam.Guard))
                        team = StartTeam.Guard;
                    else if (custom.StartTeam.HasFlag(StartTeam.Ntf))
                        team = StartTeam.Ntf;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scientist))
                        team = StartTeam.Scientist;
                    else if (custom.StartTeam.HasFlag(StartTeam.ClassD))
                        team = StartTeam.ClassD;
                    else if (custom.StartTeam.HasFlag(StartTeam.Scp))
                        team = StartTeam.Scp;
                    else
                        team = StartTeam.Other;

                    if (!CustomRoles.Plugin.Instance.Roles.ContainsKey(team))
                        CustomRoles.Plugin.Instance.Roles.Add(team, new());

                    for (int i = 0; i < role.SpawnProperties.Limit; i++)
                        CustomRoles.Plugin.Instance.Roles[team].Add(custom);
                    Log.Debug($"Roles {team} now has {CustomRoles.Plugin.Instance.Roles[team].Count} elements.");
                }
            }
            Config.HuskGrenades.Register();
            HuskInfectionEventHandlers = new HuskInfectionEventHandlers(this);
            Server.WaitingForPlayers += HuskInfectionEventHandlers.OnWaitingForPlayers;
            Server.RoundEnded += HuskInfectionEventHandlers.OnRoundEnded;
            Player.VoiceChatting += HuskInfectionEventHandlers.OnVoiceChatting;
            Player.ChangingRole += HuskInfectionEventHandlers.OnRoleChange;
        }

        public override void OnDisabled()
        {
            CustomRole.UnregisterRoles();
            CustomItem.UnregisterItems();
            Server.WaitingForPlayers -= HuskInfectionEventHandlers.OnWaitingForPlayers;
            Server.RoundEnded -= HuskInfectionEventHandlers.OnRoundEnded;
            Player.VoiceChatting -= HuskInfectionEventHandlers.OnVoiceChatting;
            Player.ChangingRole -= HuskInfectionEventHandlers.OnRoleChange;
            base.OnDisabled();
        }
    }
}